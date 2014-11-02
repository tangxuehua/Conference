using System;
using ENode.Eventing;

namespace Registration.Events
{
    public class OrderTotalsCalculated : DomainEvent<Guid>
    {
        public decimal Total { get; set; }
        public OrderLine[] Lines { get; set; }
        public bool IsFreeOfCharge { get; set; }

        public OrderTotalsCalculated(Guid sourceId) : base(sourceId) { }
    }
}
