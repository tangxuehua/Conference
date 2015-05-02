using System;
using ENode.Eventing;

namespace Registration.Orders
{
    [Serializable]
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
