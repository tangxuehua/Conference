using System;
using System.Collections.Generic;
using ENode.Eventing;

namespace Registration.Events
{
    public class OrderUpdated : DomainEvent<Guid>
    {
        public Guid ConferenceId { get; set; }
        public IEnumerable<SeatQuantity> Seats { get; set; }

        public OrderUpdated(Guid sourceId) : base(sourceId) { }
    }
}
