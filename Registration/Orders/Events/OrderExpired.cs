using System;
using ENode.Eventing;

namespace Registration.Orders
{
    [Serializable]
    public class OrderExpired : OrderEvent
    {
        public OrderExpired(Guid orderId, Guid conferenceId) : base(orderId, conferenceId) { }
    }
}
