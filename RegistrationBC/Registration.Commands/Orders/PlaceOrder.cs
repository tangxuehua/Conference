using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ENode.Commanding;
using ENode.Infrastructure;

namespace Registration.Commands.Orders
{
    [Serializable]
    [Code(3100)]
    public class PlaceOrder : Command<Guid>
    {
        public PlaceOrder() : base(Guid.NewGuid())
        {
            this.Seats = new Collection<SeatInfo>();
        }

        public Guid ConferenceId { get; set; }
        public IList<SeatInfo> Seats { get; set; }
    }
}
