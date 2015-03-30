using System;
using ENode.Eventing;

namespace Registration.SeatAssigning
{
    [Serializable]
    public class SeatUnassigned : DomainEvent<Guid>
    {
        public int Position { get; private set; }

        public SeatUnassigned(SeatAssignments seatAssignments, int position)
            : base(seatAssignments)
        {
            Position = position;
        }
    }
}
