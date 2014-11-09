using System;
using ENode.Eventing;

namespace Registration.Events
{
    public class SeatsReservationCancelled : DomainEvent<Guid>
    {
        public SeatsReservationCancelled(Guid sourceId) : base(sourceId) { }
    }
}