using System;
using ENode.Eventing;
using ENode.Infrastructure;

namespace Registration.Orders
{
    [Serializable]
    [Code(3305)]
    public class OrderSuccessed : OrderEvent
    {
        public OrderSuccessed() { }
        public OrderSuccessed(Order order, Guid conferenceId) : base(order, conferenceId) { }
    }
}
