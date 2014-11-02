using System;
using System.Collections.Generic;
using ENode.Commanding;

namespace Registration.Commands
{
    public class MarkSeatsAsReserved : AggregateCommand<Guid>
    {
        public List<SeatQuantity> Seats { get; set; }

        public MarkSeatsAsReserved(Guid orderId) : base(orderId)
        {
            this.Seats = new List<SeatQuantity>();
        }
    }
}
