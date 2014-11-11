using System;
using ENode.Eventing;

namespace Registration.SeatAvailabilities
{
    [Serializable]
    public class SeatsReservationCommitted : DomainEvent<Guid>
    {
        public Guid OrderId { get; set; }

        public SeatsReservationCommitted(Guid sourceId) : base(sourceId) { }
    }
}