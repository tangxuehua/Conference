using System;
using System.Linq;
using ConferenceManagement.Commands;
using ConferenceManagement.Domain.Models;
using ConferenceManagement.Domain.Services;
using ECommon.Components;
using ENode.Commanding;
using ENode.Infrastructure;

namespace ConferenceManagement.CommandHandlers
{
    [Component]
    public class ConferenceCommandHandler :
        ICommandHandler<CreateConference>,
        ICommandHandler<UpdateConference>,
        ICommandHandler<PublishConference>,
        ICommandHandler<UnpublishConference>,
        ICommandHandler<AddSeatType>,
        ICommandHandler<RemoveSeatType>,
        ICommandHandler<UpdateSeatType>,
        ICommandHandler<MakeSeatReservation>,
        ICommandHandler<CommitSeatReservation>,
        ICommandHandler<CancelSeatReservation>
    {
        private readonly ILockService _lockService;
        private readonly RegisterConferenceSlugService _registerConferenceSlugService;

        public ConferenceCommandHandler(ILockService lockService, RegisterConferenceSlugService registerConferenceSlugService)
        {
            _lockService = lockService;
            _registerConferenceSlugService = registerConferenceSlugService;
        }

        public void Handle(ICommandContext context, CreateConference command)
        {
            _lockService.ExecuteInLock(typeof(ConferenceSlugIndex).Name, () =>
            {
                var conference = new Conference(command.AggregateRootId, new ConferenceInfo(
                    command.AccessCode,
                    new ConferenceOwner(command.OwnerName, command.OwnerEmail),
                    command.Slug,
                    command.Name,
                    command.Description,
                    command.Location,
                    command.Tagline,
                    command.TwitterSearch,
                    command.StartDate,
                    command.EndDate));
                _registerConferenceSlugService.RegisterSlug(command.Id, conference.Id, command.Slug);
                context.Add(conference);
            });
        }
        public void Handle(ICommandContext context, UpdateConference command)
        {
            context.Get<Conference>(command.AggregateRootId).Update(new ConferenceInfo(
                command.AccessCode,
                new ConferenceOwner(command.OwnerName, command.OwnerEmail),
                command.Slug,
                command.Name,
                command.Description,
                command.Location,
                command.Tagline,
                command.TwitterSearch,
                command.StartDate,
                command.EndDate));
        }
        public void Handle(ICommandContext context, PublishConference command)
        {
            context.Get<Conference>(command.AggregateRootId).Publish();
        }
        public void Handle(ICommandContext context, UnpublishConference command)
        {
            context.Get<Conference>(command.AggregateRootId).Unpublish();
        }
        public void Handle(ICommandContext context, AddSeatType command)
        {
            context.Get<Conference>(command.AggregateRootId).AddSeat(new SeatTypeInfo(
                command.Name,
                command.Description,
                command.Price), command.Quantity);
        }
        public void Handle(ICommandContext context, RemoveSeatType command)
        {
            context.Get<Conference>(command.AggregateRootId).RemoveSeat(command.SeatTypeId);
        }
        public void Handle(ICommandContext context, UpdateSeatType command)
        {
            context.Get<Conference>(command.AggregateRootId).UpdateSeat(
                command.SeatTypeId,
                new SeatTypeInfo(command.Name, command.Description, command.Price),
                command.Quantity);
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
