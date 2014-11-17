using System.Linq;
using ConferenceManagement.Commands;
using ECommon.Components;
using ENode.Commanding;

namespace ConferenceManagement.CommandHandlers
{
    [Component]
    public class ConferenceCommandHandler :
        ICommandHandler<PublishConference>,
        ICommandHandler<UnpublishConference>,
        ICommandHandler<MakeSeatReservation>,
        ICommandHandler<CommitSeatReservation>,
        ICommandHandler<CancelSeatReservation>
    {
        public void Handle(ICommandContext context, PublishConference command)
        {
            context.Get<Conference>(command.AggregateRootId).Publish();
        }
        public void Handle(ICommandContext context, UnpublishConference command)
        {
            context.Get<Conference>(command.AggregateRootId).Unpublish();
        }
        public void Handle(ICommandContext context, MakeSeatReservation command)
        {
            context.Get<Conference>(command.AggregateRootId).MakeReservation(command.ReservationId, command.Seats.Select(x => new ReservationItem(x.SeatType, x.Quantity)).ToList());
        }
        public void Handle(ICommandContext context, CommitSeatReservation command)
        {
            context.Get<Conference>(command.AggregateRootId).CommitReservation(command.ReservationId);
        }
        public void Handle(ICommandContext context, CancelSeatReservation command)
        {
            context.Get<Conference>(command.AggregateRootId).CancelReservation(command.ReservationId);
        }
    }
}
