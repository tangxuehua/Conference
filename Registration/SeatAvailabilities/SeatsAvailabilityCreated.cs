using System;
using ENode.Eventing;

namespace Registration.SeatAvailabilities
{
    public class SeatsAvailabilityCreated : DomainEvent<Guid>
    {
        public SeatsAvailabilityCreated(Guid sourceId) : base(sourceId) { }
    }
}