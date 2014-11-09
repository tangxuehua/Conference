using System;
using ENode.Eventing;

namespace Registration.SeatAvailabilities
{
    [Serializable]
    public class SeatsReservationCancelled : DomainEvent<Guid>
    {
        public SeatsReservationCancelled(Guid sourceId) : base(sourceId) { }
    }
}