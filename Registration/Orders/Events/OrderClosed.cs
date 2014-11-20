using System;
using ENode.Eventing;

namespace Registration.Orders
{
    [Serializable]
    public class OrderClosed : OrderEvent
    {
        public OrderClosed(Guid orderId, Guid conferenceId) : base(orderId, conferenceId) { }
    }
}
