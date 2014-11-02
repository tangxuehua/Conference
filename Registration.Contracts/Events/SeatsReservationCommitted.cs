using System;
using ENode.Eventing;

namespace Registration.Events
{
    public class SeatsReservationCommitted : DomainEvent<Guid>
    {
        public SeatsReservationCommitted(Guid sourceId) : base(sourceId) { }
    }
}