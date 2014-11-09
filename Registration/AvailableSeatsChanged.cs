using System;
using System.Collections.Generic;
using ENode.Eventing;

namespace Registration
{
    public class AvailableSeatsChanged : DomainEvent<Guid>
    {
        public IEnumerable<SeatQuantity> Seats { get; private set; }

        public AvailableSeatsChanged(Guid sourceId, IEnumerable<SeatQuantity> seats) : base(sourceId)
        {
            Seats = seats;
        }
    }
}