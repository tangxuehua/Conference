using ENode.Commanding;
using Registration.Commands;

namespace Registration.CommandHandlers
{
    public class SeatsAvailabilityCommandHandler :
        ICommandHandler<CreateSeatsAvailability>,
        ICommandHandler<AddSeats>,
        ICommandHandler<RemoveSeats>,
        ICommandHandler<MakeSeatReservation>,
        ICommandHandler<CancelSeatReservation>,
        ICommandHandler<CommitSeatReservation>
    {
        public void Handle(ICommandContext context, CreateSeatsAvailability command)
        {
            context.Add(new SeatsAvailability(command.AggregateRootId));
        }
        public void Handle(ICommandContext context, AddSeats command)
        {
            context.Get<SeatsAvailability>(command.AggregateRootId).AddSeats(command.SeatType, command.Quantity);
        }
        public void Handle(ICommandContext context, RemoveSeats command)
        {
            context.Get<SeatsAvailability>(command.AggregateRootId).RemoveSeats(command.SeatType, command.Quantity);
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
