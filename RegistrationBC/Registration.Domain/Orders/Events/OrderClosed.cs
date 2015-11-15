using System;
using ENode.Eventing;
using ENode.Infrastructure;

namespace Registration.Orders
{
    [Serializable]
    [Code(3303)]
    public class OrderClosed : OrderEvent
    {
        public OrderClosed() { }
        public OrderClosed(Order order, Guid conferenceId) : base(order, conferenceId) { }
    }
}
