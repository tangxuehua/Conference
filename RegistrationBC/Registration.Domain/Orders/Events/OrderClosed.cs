using System;
using ENode.Eventing;

namespace Registration.Orders
{
    [Serializable]
    public class OrderClosed : OrderEvent
    {
        public OrderClosed() { }
        public OrderClosed(Guid conferenceId) : base(conferenceId) { }
    }
}
