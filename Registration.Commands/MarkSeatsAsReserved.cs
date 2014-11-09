using System;
using System.Collections.Generic;
using ENode.Commanding;

namespace Registration.Commands
{
    public class MarkSeatsAsReserved : AggregateCommand<Guid>
    {
        public IEnumerable<SeatInfo> Seats { get; set; }

        public MarkSeatsAsReserved(Guid orderId) : base(orderId)
        {
            this.Seats = new List<SeatInfo>();
        }
    }
}
