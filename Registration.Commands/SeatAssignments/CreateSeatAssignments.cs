using System;
using ENode.Commanding;

namespace Registration.Commands.SeatAssignments
{
    [Serializable]
    public class CreateSeatAssignments : AggregateCommand<Guid>, ICreatingAggregateCommand
    {
        public CreateSeatAssignments(Guid orderId) : base(orderId) { }
    }
}
