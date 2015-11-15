using System;
using ENode.Commanding;
using ENode.Infrastructure;

namespace Registration.Commands.SeatAssignments
{
    [Serializable]
    [Code(3108)]
    public class UnassignSeat : Command<Guid>
    {
        public int Position { get; set; }

        public UnassignSeat() { }
        public UnassignSeat(Guid orderId) : base(orderId) { }
    }
}
