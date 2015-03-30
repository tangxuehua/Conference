using System;
using ENode.Eventing;

namespace Registration.Orders
{
    [Serializable]
    public abstract class OrderEvent : DomainEvent<Guid>
    {
        public Guid ConferenceId { get; set; }

        public OrderEvent(Order order, Guid conferenceId) : base(order)
        {
            ConferenceId = conferenceId;
        }
    }
}
