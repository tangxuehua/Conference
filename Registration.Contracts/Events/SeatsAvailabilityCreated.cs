using System;
using ENode.Eventing;

namespace Registration.Events
{
    public class SeatsAvailabilityCreated : DomainEvent<Guid>
    {
        public SeatsAvailabilityCreated(Guid sourceId) : base(sourceId) { }
    }
}