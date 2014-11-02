using System;
using ENode.Eventing;

namespace Payments.Contracts.Events
{
    public class PaymentAccepted : DomainEvent<Guid>
    {
        public Guid SourceOrderId { get; set; }

        public PaymentAccepted(Guid sourceId) : base(sourceId) { }
    }
}
