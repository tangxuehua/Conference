using System;
using ENode.Eventing;
using ENode.Infrastructure;

namespace Registration.Orders
{
    [Serializable]
    [Code(3302)]
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
