using System;
using ENode.Eventing;

namespace Registration.Orders
{
    [Serializable]
    public class OrderSuccessed : OrderEvent
    {
        public OrderSuccessed() { }
        public OrderSuccessed(Guid conferenceId) : base(conferenceId) { }
    }
}
