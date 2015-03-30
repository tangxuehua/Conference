using System;
using ENode.Eventing;

namespace Registration.Orders
{
    [Serializable]
    public class OrderSuccessed : OrderEvent
    {
        public OrderSuccessed(Order order, Guid conferenceId) : base(order, conferenceId) { }
    }
}
