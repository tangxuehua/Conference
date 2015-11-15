using System;
using ENode.Commanding;
using ENode.Infrastructure;

namespace Registration.Commands.SeatAssignments
{
    [Serializable]
    [Code(3106)]
    public class AssignSeat : Command<Guid>
    {
        public int Position { get; set; }
        public PersonalInfo PersonalInfo { get; set; }

        public AssignSeat() { }
        public AssignSeat(Guid orderId) : base(orderId) { }
    }
}
