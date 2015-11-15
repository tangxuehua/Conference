using System;
using ENode.Eventing;
using ENode.Infrastructure;

namespace Registration.Orders
{
    [Serializable]
    [Code(3304)]
    public class OrderExpired : OrderEvent
    {
        public OrderExpired() { }
        public OrderExpired(Order order, Guid conferenceId) : base(order, conferenceId) { }
    }
}
