using ECommon.Components;
using ENode.Commanding;
using Registration.Commands.SeatAssignments;
using Registration.Orders;
using Registration.SeatAssigning;

namespace Registration.CommandHandlers
{
    [Component]
    public class OrderSeatAssignmentsCommandHandler :
        ICommandHandler<CreateSeatAssignments>,
        ICommandHandler<UnassignSeat>,
        ICommandHandler<AssignSeat>
    {
        public void Handle(ICommandContext context, CreateSeatAssignments command)
        {
            context.Add(context.Get<Order>(command.AggregateRootId).CreateSeatAssignments());
        }
        public void Handle(ICommandContext context, AssignSeat command)
        {
            context.Get<OrderSeatAssignments>(command.AggregateRootId).AssignSeat(command.Position, new Attendee(command.PersonalInfo.FirstName, command.PersonalInfo.LastName, command.PersonalInfo.Email));
        }
        public void Handle(ICommandContext context, UnassignSeat command)
        {
            context.Get<OrderSeatAssignments>(command.AggregateRootId).UnassignSeat(command.Position);
        }
    }
}
