using System;
using ENode.Eventing;

namespace Registration.SeatAssigning
{
    [Serializable]
    public class SeatUnassigned : DomainEvent<Guid>
    {
        public int Position { get; private set; }

        public SeatUnassigned(Guid assignmentsId, int position) : base(assignmentsId)
        {
            Position = position;
        }
    }
}
