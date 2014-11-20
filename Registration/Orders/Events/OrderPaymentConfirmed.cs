using System;
using ENode.Eventing;

namespace Registration.Orders
{
    [Serializable]
    public class OrderPaymentConfirmed : OrderEvent
    {
        public OrderPaymentConfirmed(Guid orderId, Guid conferenceId) : base(orderId, conferenceId) { }
    }
}
