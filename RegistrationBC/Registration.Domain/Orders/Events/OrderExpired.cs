using System;
using ENode.Eventing;

namespace Registration.Orders
{
    [Serializable]
    public class OrderExpired : OrderEvent
    {
        public OrderExpired() { }
        public OrderExpired(Guid conferenceId) : base(conferenceId) { }
    }
}
