using System;
using ENode.Eventing;

namespace Registration.Events
{
    public class SeatUnassigned : DomainEvent<Guid>
    {
        public int Position { get; private set; }

        public SeatUnassigned(Guid sourceId, int position) : base(sourceId)
        {
            Position = position;
        }
    }
}
