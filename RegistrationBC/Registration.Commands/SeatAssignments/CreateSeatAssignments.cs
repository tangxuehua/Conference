using System;
using ENode.Commanding;
using ENode.Infrastructure;

namespace Registration.Commands.SeatAssignments
{
    [Serializable]
    [Code(3107)]
    public class CreateSeatAssignments : Command<Guid>
    {
        public CreateSeatAssignments() { }
        public CreateSeatAssignments(Guid orderId) : base(orderId) { }
    }
}
