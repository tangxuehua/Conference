using System;
using ENode.Commanding;

namespace Registration.Commands
{
    public class UnassignSeat : AggregateCommand<Guid>
    {
        public int Position { get; set; }

        public UnassignSeat(Guid seatAssignmentsId) : base(seatAssignmentsId) { }
    }
}
