using System;
using ENode.Eventing;

namespace Registration.SeatAvailabilities
{
    [Serializable]
    public class SeatsReservationCancelled : DomainEvent<Guid>
    {
        public Guid OrderId { get; set; }

        public SeatsReservationCancelled(Guid sourceId) : base(sourceId) { }
    }
}