using System;
using System.Collections.Generic;
using ENode.Eventing;

namespace Registration.Events
{
    public class SeatsReservationCancelled : DomainEvent<Guid>
    {
        public IEnumerable<SeatQuantity> AvailableSeatsChanged { get; set; }

        public SeatsReservationCancelled(Guid sourceId) : base(sourceId) { }
    }
}