using System;
using ENode.Eventing;
using ENode.Infrastructure;

namespace Payments
{
    [Serializable]
    [Code(2301)]
    public class PaymentCompleted : DomainEvent<Guid>
    {
        public Guid OrderId { get; private set; }
        public Guid ConferenceId { get; private set; }

        public PaymentCompleted() { }
        public PaymentCompleted(Payment payment, Guid orderId, Guid conferenceId)
            : base(payment)
        {
            OrderId = orderId;
            ConferenceId = conferenceId;
        }
    }
}
