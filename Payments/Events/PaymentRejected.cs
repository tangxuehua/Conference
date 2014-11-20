using System;
using ENode.Eventing;

namespace Payments
{
    [Serializable]
    public class PaymentRejected : DomainEvent<Guid>
    {
        public Guid OrderId { get; private set; }
        public Guid ConferenceId { get; private set; }

        public PaymentRejected(Guid paymentId, Guid orderId, Guid conferenceId) : base(paymentId)
        {
            OrderId = orderId;
            ConferenceId = conferenceId;
        }
    }
}
