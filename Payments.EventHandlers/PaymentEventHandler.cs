using ECommon.Components;
using ENode.Eventing;
using Payments.Contracts;

namespace Payments.EventHandlers
{
    [Component]
    public class PaymentEventHandler :
        IEventHandler<PaymentInitiated>,
        IEventHandler<PaymentCompleted>,
        IEventHandler<PaymentRejected>
    {
        private IEventPublisher _eventPublisher;

        public PaymentEventHandler(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public void Handle(IEventContext context, PaymentInitiated evnt)
        {
            _eventPublisher.Publish(new PaymentInitiatedEvent
            {
                AggregateRootId = evnt.AggregateRootId,
                Version = evnt.Version,
                Timestamp = evnt.Timestamp,
                ConferenceId = evnt.ConferenceId,
                OrderId = evnt.OrderId
            });
        }
        public void Handle(IEventContext context, PaymentCompleted evnt)
        {
            _eventPublisher.Publish(new PaymentCompletedEvent
            {
                AggregateRootId = evnt.AggregateRootId,
                Version = evnt.Version,
                Timestamp = evnt.Timestamp,
                ConferenceId = evnt.ConferenceId,
                OrderId = evnt.OrderId
            });
        }
        public void Handle(IEventContext context, PaymentRejected evnt)
        {
            _eventPublisher.Publish(new PaymentRejectedEvent
            {
                AggregateRootId = evnt.AggregateRootId,
                Version = evnt.Version,
                Timestamp = evnt.Timestamp,
                ConferenceId = evnt.ConferenceId,
                OrderId = evnt.OrderId
            });
        }
    }
}
