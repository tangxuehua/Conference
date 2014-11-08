using System;
using ENode.Eventing;

namespace Payments.Contracts
{
    [Serializable]
    public abstract class PaymentEvent : DomainEvent<Guid>
    {
        public Guid OrderId { get; set; }
        public Guid ConferenceId { get; set; }
    }
}
