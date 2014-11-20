using System.Linq;
using ConferenceManagement.Messages;
using ECommon.Components;
using ENode.Eventing;
using ENode.Exceptions;
using ENode.Infrastructure;
using ENode.Messaging;

namespace ConferenceManagement.MessagePublishers
{
    [Component]
    public class ConferenceMessagePublisher :
        IEventHandler<SeatsReserved>,
        IEventHandler<SeatsReservationCommitted>,
        IEventHandler<SeatsReservationCancelled>,
        IExceptionHandler<SeatInsufficientException>
    {
        private readonly IPublisher<IMessage> _messagePublisher;

        public ConferenceMessagePublisher(IPublisher<IMessage> messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        public void Handle(IHandlingContext context, SeatsReserved evnt)
        {
            _messagePublisher.Publish(new SeatsReservedMessage
            {
                SourceId = evnt.AggregateRootId,
                Version = evnt.Version,
                Timestamp = evnt.Timestamp,
                ReservationId = evnt.ReservationId,
                ReservationItems = evnt.ReservationItems.Select(x => new SeatReservationItem { SeatTypeId = x.SeatTypeId, Quantity = x.Quantity }).ToList()
            });
        }
        public void Handle(IHandlingContext context, SeatsReservationCommitted evnt)
        {
            _messagePublisher.Publish(new SeatsReservationCommittedMessage
            {
                SourceId = evnt.AggregateRootId,
                Version = evnt.Version,
                Timestamp = evnt.Timestamp,
                ReservationId = evnt.ReservationId
            });
        }
        public void Handle(IHandlingContext context, SeatsReservationCancelled evnt)
        {
            _messagePublisher.Publish(new SeatsReservationCancelledMessage
            {
                SourceId = evnt.AggregateRootId,
                Version = evnt.Version,
                Timestamp = evnt.Timestamp,
                ReservationId = evnt.ReservationId
            });
        }
        public void Handle(IHandlingContext context, SeatInsufficientException exception)
        {
            _messagePublisher.Publish(new SeatInsufficientMessage
            {
                Id = exception.UniqueId,
                ConferenceId = exception.ConferenceId,
                ReservationId = exception.ReservationId
            });
        }
    }
}
