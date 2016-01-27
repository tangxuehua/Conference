using System;
using ENode.Commanding;

namespace Registration.Commands.SeatAssignments
{
    [Serializable]
    public class CreateSeatAssignments : Command<Guid>
    {
        public CreateSeatAssignments() { }
        public CreateSeatAssignments(Guid orderId) : base(orderId) { }
    }
}
