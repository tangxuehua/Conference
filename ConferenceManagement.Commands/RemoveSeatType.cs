using System;
using ENode.Commanding;

namespace ConferenceManagement.Commands
{
    [Serializable]
    public class RemoveSeatType : AggregateCommand<Guid>
    {
        public Guid SeatTypeId { get; set; }

        public RemoveSeatType(Guid conferenceId) : base(conferenceId) { }
    }
}
