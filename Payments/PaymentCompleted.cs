using System;
using ENode.Eventing;

namespace Payments
{
    [Serializable]
    public class PaymentCompleted : DomainEvent<Guid>
    {
        public Guid OrderId { get; private set; }
        public Guid ConferenceId { get; private set; }

        public PaymentCompleted(Guid paymentId, Guid orderId, Guid conferenceId) : base(paymentId)
        {
            OrderId = orderId;
            ConferenceId = conferenceId;
        }
    }
}
