using System;
using ENode.Commanding;

namespace Registration.Commands
{
    public class CreateSeatAssignments : AggregateCommand<Guid>, ICreatingAggregateCommand
    {
        public CreateSeatAssignments(Guid orderId) : base(orderId) { }
    }
}
