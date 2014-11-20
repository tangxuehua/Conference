using ECommon.Components;
using ENode.Eventing;
using ENode.Infrastructure;
using ENode.Messaging;
using Payments.Messages;

namespace Payments.EventHandlers
{
    [Component]
    public class PaymentMessagePublisher :
        IEventHandler<PaymentInitiated>,
        IEventHandler<PaymentCompleted>,
        IEventHandler<PaymentRejected>
    {
        private readonly IPublisher<IMessage> _messagePublisher;

        public PaymentMessagePublisher(IPublisher<IMessage> messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        public void Handle(IHandlingContext context, PaymentInitiated evnt)
        {
            _messagePublisher.Publish(new PaymentInitiatedMessage
            {
                SourceId = evnt.AggregateRootId,
                Version = evnt.Version,
                Timestamp = evnt.Timestamp,
                ConferenceId = evnt.ConferenceId,
                OrderId = evnt.OrderId
            });
        }
        public void Handle(IHandlingContext context, PaymentCompleted evnt)
        {
            _messagePublisher.Publish(new PaymentCompletedMessage
            {
                SourceId = evnt.AggregateRootId,
                Version = evnt.Version,
                Timestamp = evnt.Timestamp,
                ConferenceId = evnt.ConferenceId,
                OrderId = evnt.OrderId
            });
        }
        public void Handle(IHandlingContext context, PaymentRejected evnt)
        {
            _messagePublisher.Publish(new PaymentRejectedMessage
            {
                SourceId = evnt.AggregateRootId,
                Version = evnt.Version,
                Timestamp = evnt.Timestamp,
                ConferenceId = evnt.ConferenceId,
                OrderId = evnt.OrderId
            });
        }
    }
}
