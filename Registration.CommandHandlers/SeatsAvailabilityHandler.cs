using ENode.Commanding;
using Registration.Commands;

namespace Registration.CommandHandlers
{
    public class SeatsAvailabilityHandler :
        ICommandHandler<MakeSeatReservation>,
        ICommandHandler<CancelSeatReservation>,
        ICommandHandler<CommitSeatReservation>,
        ICommandHandler<AddSeats>,
        ICommandHandler<RemoveSeats>
    {
        public void Handle(ICommandContext context, MakeSeatReservation command)
        {
            var availability = context.Get<SeatsAvailability>(command.AggregateRootId);
            availability.MakeReservation(command.ReservationId, command.Seats);
        }

        public void Handle(ICommandContext context, CancelSeatReservation command)
        {
            context.Get<SeatsAvailability>(command.AggregateRootId).CancelReservation(command.ReservationId);
        }

        public void Handle(ICommandContext context, CommitSeatReservation command)
        {
            context.Get<SeatsAvailability>(command.AggregateRootId).CommitReservation(command.ReservationId);
        }

        // Commands created from events from the conference BC

        public void Handle(ICommandContext context, AddSeats command)
        {
            //TODO
            //var availability = this.repository.Find(command.ConferenceId);
            //if (availability == null)
            //    availability = new SeatsAvailability(command.ConferenceId);

            //availability.AddSeats(command.SeatType, command.Quantity);
        }

        public void Handle(ICommandContext context, RemoveSeats command)
        {
            //TODO
            //var availability = this.repository.Find(command.ConferenceId);
            //if (availability == null)
            //    availability = new SeatsAvailability(command.ConferenceId);

            //availability.RemoveSeats(command.SeatType, command.Quantity);
        }
    }
}
