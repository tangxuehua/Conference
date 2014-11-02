using System;
using ENode.Eventing;

namespace Payments.Contracts.Events
{
    public class PaymentCompleted : DomainEvent<Guid>
    {
        public Guid SourceOrderId { get; set; }

        public PaymentCompleted(Guid sourceId) : base(sourceId) { }
    }
}
