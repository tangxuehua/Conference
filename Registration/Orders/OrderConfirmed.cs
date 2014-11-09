using System;
using ENode.Eventing;

namespace Registration.Orders
{
    [Serializable]
    public class OrderConfirmed : DomainEvent<Guid>
    {
        public Guid ConferenceId { get; set; }

        public OrderConfirmed(Guid sourceId) : base(sourceId) { }
    }
}
