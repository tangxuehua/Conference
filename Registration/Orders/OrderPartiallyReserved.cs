using System;
using System.Collections.Generic;
using ENode.Eventing;

namespace Registration.Orders
{
    public class OrderPartiallyReserved : DomainEvent<Guid>
    {
        public IEnumerable<SeatQuantity> Seats { get; private set; }

        public OrderPartiallyReserved(Guid sourceId, IEnumerable<SeatQuantity> seats) : base(sourceId)
        {
            Seats = seats;
        }
    }
}
