using System.Data;
using System.Data.SqlClient;
using Conference.Common;
using ECommon.Components;
using ECommon.Dapper;
using ENode.Eventing;
using ENode.Infrastructure;
using Registration.Orders;

namespace Registration.Handlers
{
    [Component]
    public class OrderViewModelGenerator :
        IEventHandler<OrderPlaced>,
        IEventHandler<OrderReservationConfirmed>,
        IEventHandler<OrderPaymentConfirmed>,
        IEventHandler<OrderExpired>,
        IEventHandler<OrderClosed>,
        IEventHandler<OrderSuccessed>
    {
        public void Handle(IHandlingContext context, OrderPlaced evnt)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    //插入订单记录
                    connection.Insert(
                        new
                        {
                            OrderId = evnt.AggregateRootId,
                            ConferenceId = evnt.ConferenceId,
                            Status = (int)OrderStatus.Placed,
                            AccessCode = evnt.AccessCode,
                            RegistrantEmail = evnt.Registrant.Email,
                            RegistrantFirstName = evnt.Registrant.FirstName,
                            RegistrantLastName = evnt.Registrant.LastName,
                            ReservationAutoExpiration = evnt.ReservationAutoExpiration,
                            Total = evnt.OrderTotal.Total
                        } , ConfigSettings.OrderTable, transaction);

                    //插入订单明细
                    foreach (var line in evnt.OrderTotal.Lines)
                    {
                        connection.Insert(
                            new
                            {
                                OrderId = evnt.AggregateRootId,
                                SeatTypeId = line.SeatQuantity.Seat.SeatTypeId,
                                SeatTypeName = line.SeatQuantity.Seat.SeatTypeName,
                                Quantity = line.SeatQuantity.Quantity,
                                UnitPrice = line.SeatQuantity.Seat.UnitPrice,
                                LineTotal = line.LineTotal
                            }, ConfigSettings.OrderLineTable, transaction);
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
        public void Handle(IHandlingContext context, OrderReservationConfirmed evnt)
        {
            var status = 0;
            if (evnt.IsReservationSuccess)
            {
                status = (int)OrderStatus.ReservationSuccess;
            }
            else
            {
                status = (int)OrderStatus.ReservationFailed;
            }

            using (var connection = GetConnection())
            {
                connection.Update(new { Status = status }, new { OrderId = evnt.AggregateRootId }, ConfigSettings.OrderTable);
            }
        }
        public void Handle(IHandlingContext context, OrderPaymentConfirmed evnt)
        {
            var status = 0;
            if (evnt.IsPaymentSuccess)
            {
                status = (int)OrderStatus.PaymentSuccess;
            }
            else
            {
                status = (int)OrderStatus.PaymentRejected;
            }

            using (var connection = GetConnection())
            {
                connection.Update(new { Status = status }, new { OrderId = evnt.AggregateRootId }, ConfigSettings.OrderTable);
            }
        }
        public void Handle(IHandlingContext context, OrderExpired evnt)
        {
            using (var connection = GetConnection())
            {
                connection.Update(new { Status = (int)OrderStatus.Expired }, new { OrderId = evnt.AggregateRootId }, ConfigSettings.OrderTable);
            }
        }
        public void Handle(IHandlingContext context, OrderClosed evnt)
        {
            using (var connection = GetConnection())
            {
                connection.Update(new { Status = (int)OrderStatus.Closed }, new { OrderId = evnt.AggregateRootId }, ConfigSettings.OrderTable);
            }
        }
        public void Handle(IHandlingContext context, OrderSuccessed evnt)
        {
            using (var connection = GetConnection())
            {
                connection.Update(new { Status = (int)OrderStatus.Success }, new { OrderId = evnt.AggregateRootId }, ConfigSettings.OrderTable);
            }
        }

        private IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigSettings.ConnectionString);
        }
    }
}
