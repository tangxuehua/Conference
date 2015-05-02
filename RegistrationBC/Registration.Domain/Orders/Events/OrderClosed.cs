using System;
using ENode.Eventing;

namespace Registration.Orders
{
    [Serializable]
    public class OrderClosed : OrderEvent
    {
        public OrderClosed() { }
        public OrderClosed(Order order, Guid conferenceId) : base(order, conferenceId) { }
    }
}
