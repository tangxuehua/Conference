using System;
using ENode.Eventing;
using ENode.Infrastructure;

namespace Registration.Orders
{
    [Serializable]
    [Code(3301)]
    public class OrderReservationConfirmed : OrderEvent
    {
        public OrderStatus OrderStatus { get; private set; }

        public OrderReservationConfirmed() { }
        public OrderReservationConfirmed(Order order, Guid conferenceId, OrderStatus orderStatus)
            : base(order, conferenceId)
        {
            OrderStatus = orderStatus;
        }
    }
}
