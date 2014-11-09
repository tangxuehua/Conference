using System;
using ENode.Eventing;

namespace Registration.SeatAvailabilities
{
    public class SeatsReservationCancelled : DomainEvent<Guid>
    {
        public SeatsReservationCancelled(Guid sourceId) : base(sourceId) { }
    }
}