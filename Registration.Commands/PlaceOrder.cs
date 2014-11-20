using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ENode.Commanding;

namespace Registration.Commands
{
    [Serializable]
    public class PlaceOrder : AggregateCommand<Guid>, ICreatingAggregateCommand
    {
        public PlaceOrder() : base(Guid.NewGuid())
        {
            this.Seats = new Collection<SeatInfo>();
        }

        public Guid ConferenceId { get; set; }
        public ICollection<SeatInfo> Seats { get; set; }
    }
}
