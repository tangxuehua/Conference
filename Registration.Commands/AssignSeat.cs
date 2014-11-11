using System;
using ENode.Commanding;

namespace Registration.Commands
{
    [Serializable]
    public class AssignSeat : AggregateCommand<Guid>
    {
        public int Position { get; set; }
        public PersonalInfo Attendee { get; set; }

        public AssignSeat(Guid seatAssignmentsId) : base(seatAssignmentsId) { }
    }
}
