using System;
using System.Collections.Generic;
using ENode.Eventing;

namespace Registration.Events
{
    public class AvailableSeatsChanged : DomainEvent<Guid>
    {
        public IEnumerable<SeatQuantity> Seats { get; set; }

        public AvailableSeatsChanged(Guid sourceId) : base(sourceId) { }
    }
}