using System;
using ENode.Eventing;

namespace Registration.Orders
{
    [Serializable]
    public class OrderSuccessed : OrderEvent
    {
        public OrderSuccessed(Guid orderId, Guid conferenceId) : base(orderId, conferenceId) { }
    }
}
