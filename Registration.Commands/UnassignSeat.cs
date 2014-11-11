using System;
using ENode.Commanding;

namespace Registration.Commands
{
    [Serializable]
    public class UnassignSeat : AggregateCommand<Guid>
    {
        public int Position { get; set; }

        public UnassignSeat(Guid seatAssignmentsId) : base(seatAssignmentsId) { }
    }
}
