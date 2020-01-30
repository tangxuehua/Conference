using System.Threading.Tasks;
using ECommon.Components;
using ENode.Messaging;
using Payments.Messages;

namespace Payments.MessagePublishers
{
    [Component]
    public class PaymentMessagePublisher :
        IMessageHandler<PaymentCompleted>,
        IMessageHandler<PaymentRejected>
    {
        private readonly IMessagePublisher<IApplicationMessage> _messagePublisher;

        public PaymentMessagePublisher(IMessagePublisher<IApplicationMessage> messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        public Task HandleAsync(PaymentCompleted evnt)
        {
            return _messagePublisher.PublishAsync(new PaymentCompletedMessage
            {
                PaymentId = evnt.AggregateRootId,
                ConferenceId = evnt.ConferenceId,
                OrderId = evnt.OrderId
            });
        }
        public Task HandleAsync(PaymentRejected evnt)
        {
            return _messagePublisher.PublishAsync(new PaymentRejectedMessage
            {
                PaymentId = evnt.AggregateRootId,
                ConferenceId = evnt.ConferenceId,
                OrderId = evnt.OrderId
            });
        }
    }
}
