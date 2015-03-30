using System;
using ENode.Eventing;

namespace Payments
{
    [Serializable]
    public class PaymentCompleted : DomainEvent<Guid>
    {
        public Guid OrderId { get; private set; }
        public Guid ConferenceId { get; private set; }

        public PaymentCompleted(Payment payment, Guid orderId, Guid conferenceId)
            : base(payment)
        {
            OrderId = orderId;
            ConferenceId = conferenceId;
        }
    }
}
