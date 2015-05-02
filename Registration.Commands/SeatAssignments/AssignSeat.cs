using System;
using ENode.Commanding;

namespace Registration.Commands.SeatAssignments
{
    [Serializable]
    public class AssignSeat : Command<Guid>
    {
        public int Position { get; set; }
        public PersonalInfo PersonalInfo { get; set; }

        public AssignSeat(Guid orderId) : base(orderId) { }
    }
}
