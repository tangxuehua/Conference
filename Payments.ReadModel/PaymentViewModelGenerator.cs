namespace Payments.ReadModel
{
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using Conference.Common;
    using ECommon.Components;
    using ECommon.Dapper;
    using ENode.Eventing;

    [Component]
    public class PaymentViewModelGenerator :
        IEventHandler<PaymentInitiated>,
        IEventHandler<PaymentCompleted>,
        IEventHandler<PaymentRejected>
    {
        public void Handle(IEventContext context, PaymentInitiated evnt)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    connection.Insert(
                    new
                    {
                        Id = evnt.AggregateRootId,
                        PaymentSourceId = evnt.OrderId,
                        StateValue = (int)PaymentState.Initiated,
                        Description = evnt.Description,
                        Amount = evnt.TotalAmount
                    }, "ThirdPartyProcessorPayments");
                    foreach (var item in evnt.Items)
                    {
                        connection.Insert(
                        new
                        {
                            Id = item.Id,
                            ThirdPartyProcessorPayment_Id = evnt.AggregateRootId,
                            Description = item.Description,
                            Amount = item.Amount
                        }, "ThidPartyProcessorPaymentItems");
                    }
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void Handle(IEventContext context, PaymentCompleted evnt)
        {
            using (var connection = GetConnection())
            {
                connection.Update(
                    new
                    {
                        StateValue = (int)PaymentState.Completed
                    },
                    new
                    {
                        Id = evnt.AggregateRootId
                    }, "ThirdPartyProcessorPayments");
            }
        }
        public void Handle(IEventContext context, PaymentRejected evnt)
        {
            using (var connection = GetConnection())
            {
                connection.Update(
                    new
                    {
                        StateValue = (int)PaymentState.Rejected
                    },
                    new
                    {
                        Id = evnt.AggregateRootId
                    }, "ThirdPartyProcessorPayments");
            }
        }

        private IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigSettings.ConnectionString);
        }
    }
}
