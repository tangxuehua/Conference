using System;
using ENode.Eventing;
using ENode.Infrastructure;

namespace Payments
{
    [Serializable]
    [Code(2302)]
    public class PaymentRejected : DomainEvent<Guid>
    {
        public Guid OrderId { get; private set; }
        public Guid ConferenceId { get; private set; }

        public PaymentRejected() { }
        public PaymentRejected(Payment payment, Guid orderId, Guid conferenceId)
            : base(payment)
        {
            OrderId = orderId;
            ConferenceId = conferenceId;
        }
    }
}
