using System;
using System.Collections.Generic;
using ENode.Eventing;

namespace Registration.Events
{
    public class OrderReservationCompleted : DomainEvent<Guid>
    {
        public IEnumerable<SeatQuantity> Seats { get; set; }

        public OrderReservationCompleted(Guid sourceId) : base(sourceId) { }
    }
}
