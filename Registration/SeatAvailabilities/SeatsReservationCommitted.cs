using System;
using ENode.Eventing;

namespace Registration.SeatAvailabilities
{
    public class SeatsReservationCommitted : DomainEvent<Guid>
    {
        public SeatsReservationCommitted(Guid sourceId) : base(sourceId) { }
    }
}