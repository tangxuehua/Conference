using System.Linq;
using ECommon.Components;
using ENode.Commanding;
using Registration.Commands;
using Registration.SeatAvailabilities;

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
                availability = new SeatsAvailability(command.AggregateRootId);
                context.Add(availability);
            }
            availability.AddSeats(new SeatQuantity(command.SeatType, command.Quantity));
        }
        public void Handle(ICommandContext context, RemoveSeats command)
        {
            context.Get<SeatsAvailability>(command.AggregateRootId).RemoveSeats(new SeatQuantity(command.SeatType, command.Quantity));
        }
        public void Handle(ICommandContext context, MakeSeatReservation command)
        {
            context.Get<SeatsAvailability>(command.AggregateRootId).MakeReservation(command.ReservationId, command.Seats.Select(x => new SeatQuantity(x.SeatType, x.Quantity)).ToList());
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
