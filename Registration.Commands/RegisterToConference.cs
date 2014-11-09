using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ENode.Commanding;

namespace Registration.Commands
{
    public class RegisterToConference : AggregateCommand<Guid>, ICreatingAggregateCommand
    {
        public RegisterToConference() : base(Guid.NewGuid())
        {
            this.Seats = new Collection<SeatInfo>();
        }

        public Guid ConferenceId { get; set; }
        public ICollection<SeatInfo> Seats { get; set; }
    }
}
