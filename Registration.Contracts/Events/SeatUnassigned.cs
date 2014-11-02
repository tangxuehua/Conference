using System;
using ENode.Eventing;

namespace Registration.Events
{
    public class SeatUnassigned : DomainEvent<Guid>
    {
        public int Position { get; set; }

        public SeatUnassigned(Guid sourceId) : base(sourceId) { }
    }
}
