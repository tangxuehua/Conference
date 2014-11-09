using System;
using System.Collections.Generic;
using ENode.Eventing;

namespace Registration.Orders
{
    public class OrderReservationCompleted : DomainEvent<Guid>
    {
        public IEnumerable<SeatQuantity> Seats { get; private set; }

        public OrderReservationCompleted(Guid sourceId, IEnumerable<SeatQuantity> seats) : base(sourceId)
        {
            Seats = seats;
        }
    }
}
