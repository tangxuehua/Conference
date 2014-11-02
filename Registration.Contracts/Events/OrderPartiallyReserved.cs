using System;
using System.Collections.Generic;
using ENode.Eventing;

namespace Registration.Events
{
    public class OrderPartiallyReserved : DomainEvent<Guid>
    {
        public IEnumerable<SeatQuantity> Seats { get; set; }

        public OrderPartiallyReserved(Guid sourceId) : base(sourceId) { }
    }
}
