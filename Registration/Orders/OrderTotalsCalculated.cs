using System;
using System.Collections.Generic;
using ENode.Eventing;

namespace Registration.Orders
{
    [Serializable]
    public class OrderTotalsCalculated : DomainEvent<Guid>
    {
        public decimal Total { get; private set; }
        public OrderLine[] Lines { get; private set; }
        public bool IsFreeOfCharge { get; private set; }

        public OrderTotalsCalculated(Guid sourceId, decimal total, OrderLine[] lines, bool isFreeOfCharge) : base(sourceId)
        {
            Total = total;
            Lines = lines;
            IsFreeOfCharge = isFreeOfCharge;
        }
    }
}
