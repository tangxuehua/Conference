using System;
using ENode.Eventing;

namespace Registration.Orders
{
    [Serializable]
    public class OrderPaymentConfirmed : OrderEvent
    {
        public OrderStatus OrderStatus { get; private set; }

        public OrderPaymentConfirmed() { }
        public OrderPaymentConfirmed(Order order, Guid conferenceId, OrderStatus orderStatus)
            : base(order, conferenceId)
        {
            OrderStatus = orderStatus;
        }
    }
}
