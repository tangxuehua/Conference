using ECommon.Components;
using ENode.Commanding;
using Registration.Commands;

namespace Registration.CommandHandlers
{
    [Component]
    public class SeatsAvailabilityCommandHandler :
        ICommandHandler<AddSeats>,
        ICommandHandler<RemoveSeats>,
        ICommandHandler<MakeSeatReservation>,
        ICommandHandler<CancelSeatReservation>,
        ICommandHandler<CommitSeatReservation>
    {
        public void Handle(ICommandContext context, AddSeats command)
        {
            var availability = context.Get<SeatsAvailability>(command.AggregateRootId);
            if (availability == null)
            {
                context.Add(new SeatsAvailability(command.AggregateRootId));
            }
            else
            {
                availability.AddSeats(command.SeatType, command.Quantity);
            }
        }
        public void Handle(ICommandContext context, RemoveSeats command)
        {
            var availability = context.Get<SeatsAvailability>(command.AggregateRootId);
            if (availability == null)
            {
                context.Add(new SeatsAvailability(command.AggregateRootId));
            }
            else
            {
                availability.RemoveSeats(command.SeatType, command.Quantity);
            }
        }
        public void Handle(ICommandContext context, MakeSeatReservation command)
        {
            context.Get<SeatsAvailability>(command.AggregateRootId).MakeReservation(command.ReservationId, command.Seats);
        }
        public void Handle(ICommandContext context, CancelSeatReservation command)
        {
            context.Get<SeatsAvailability>(command.AggregateRootId).CancelReservation(command.ReservationId);
        }
        public void Handle(ICommandContext context, CommitSeatReservation command)
        {
            context.Get<SeatsAvailability>(command.AggregateRootId).CommitReservation(command.ReservationId);
        }
    }
}
