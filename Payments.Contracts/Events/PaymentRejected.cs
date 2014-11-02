using System;
using ENode.Eventing;

namespace Payments.Contracts.Events
{
    public class PaymentRejected : DomainEvent<Guid>
    {
        public Guid SourceOrderId { get; set; }

        public PaymentRejected(Guid sourceId) : base(sourceId) { }
    }
}
