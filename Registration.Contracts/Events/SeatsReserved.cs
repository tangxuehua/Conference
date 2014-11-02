using System;
using System.Collections.Generic;
using ENode.Eventing;

namespace Registration.Events
{
    public class SeatsReserved : DomainEvent<Guid>
    {
        public IEnumerable<SeatQuantity> ReservationDetails { get; set; }
        public IEnumerable<SeatQuantity> AvailableSeatsChanged { get; set; }

        public SeatsReserved(Guid sourceId) : base(sourceId) { }
    }
}