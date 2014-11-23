namespace Payments.ReadModel
{
    using System.Data;
    using System.Data.SqlClient;
    using Conference.Common;
    using ECommon.Components;
    using ECommon.Dapper;
    using ENode.Eventing;
    using ENode.Infrastructure;

    [Component]
    public class PaymentViewModelGenerator :
        IEventHandler<PaymentInitiated>,
        IEventHandler<PaymentCompleted>,
        IEventHandler<PaymentRejected>
    {
        public void Handle(IHandlingContext context, PaymentInitiated evnt)
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
                        OrderId = evnt.OrderId,
                        State = (int)PaymentState.Initiated,
                        Description = evnt.Description,
                        Amount = evnt.TotalAmount
                    }, ConfigSettings.PaymentTable, transaction);
                    foreach (var item in evnt.Items)
                    {
                        connection.Insert(
                        new
                        {
                            Id = item.Id,
                            PaymentId = evnt.AggregateRootId,
                            Description = item.Description,
                            Amount = item.Amount
                        }, ConfigSettings.PaymentItemTable, transaction);
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
        public void Handle(IHandlingContext context, PaymentCompleted evnt)
        {
            using (var connection = GetConnection())
            {
                connection.Update(new { State = (int)PaymentState.Completed }, new { Id = evnt.AggregateRootId }, ConfigSettings.PaymentTable);
            }
        }
        public void Handle(IHandlingContext context, PaymentRejected evnt)
        {
            using (var connection = GetConnection())
            {
                connection.Update(new { State = (int)PaymentState.Rejected }, new { Id = evnt.AggregateRootId }, ConfigSettings.PaymentTable);
            }
        }

        private IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigSettings.ConnectionString);
        }
    }
}
