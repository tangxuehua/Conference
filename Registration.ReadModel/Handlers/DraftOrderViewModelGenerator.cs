using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Conference.Common;
using ECommon.Components;
using ECommon.Dapper;
using ENode.Eventing;
using Registration.Orders;
using Registration.ReadModel;

namespace Registration.Handlers
{
    [Component]
    public class DraftOrderViewModelGenerator :
        IEventHandler<OrderPlaced>,
        IEventHandler<OrderPartiallyReserved>,
        IEventHandler<OrderReservationCompleted>,
        IEventHandler<OrderRegistrantAssigned>,
        IEventHandler<OrderConfirmed>
    {
        public void Handle(IEventContext eventContext, OrderPlaced evnt)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    var order = new DraftOrder(evnt.AggregateRootId, evnt.ConferenceId, DraftOrder.States.PendingReservation, evnt.Version) { AccessCode = evnt.AccessCode, };
                    connection.Insert(order, "OrdersViewV3", transaction);
                    foreach (var seat in evnt.Seats)
                    {
                        connection.Insert(new DraftOrderItem(seat.SeatType, seat.Quantity) { OrderId = evnt.AggregateRootId }, "OrderItemsViewV3", transaction);
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
        public void Handle(IEventContext eventContext, OrderRegistrantAssigned evnt)
        {
            using (var connection = GetConnection())
            {
                connection.Update(new { RegistrantEmail = evnt.Registrant.Email, OrderVersion = evnt.Version }, new { OrderId = evnt.AggregateRootId }, "OrdersViewV3");
            }
        }
        public void Handle(IEventContext eventContext, OrderPartiallyReserved evnt)
        {
            UpdateReserved(evnt.AggregateRootId, DraftOrder.States.PartiallyReserved, evnt.Version, evnt.Seats);
        }
        public void Handle(IEventContext eventContext, OrderReservationCompleted evnt)
        {
            UpdateReserved(evnt.AggregateRootId, DraftOrder.States.ReservationCompleted, evnt.Version, evnt.Seats);
        }
        public void Handle(IEventContext eventContext, OrderConfirmed evnt)
        {
            using (var connection = GetConnection())
            {
                connection.Update(new { State = DraftOrder.States.Confirmed }, new { OrderId = evnt.AggregateRootId }, "OrdersViewV3");
            }
        }

        private void UpdateReserved(Guid orderId, DraftOrder.States state, int orderVersion, IEnumerable<SeatQuantity> seats)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    connection.Update(new { State = state }, new { OrderId = orderId }, "OrdersViewV3", transaction);
                    foreach (var seat in seats)
                    {
                        connection.Update(new { ReservedSeats = seat.Quantity }, new { OrderId = orderId }, "OrderItemsViewV3", transaction);
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
        private IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigSettings.ConnectionString);
        }
    }
}
