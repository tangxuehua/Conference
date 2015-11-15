using System;
using ENode.Commanding;
using ENode.Infrastructure;

namespace ConferenceManagement.Commands
{
    [Serializable]
    [Code(1106)]
    public class RemoveSeatType : Command<Guid>
    {
        public Guid SeatTypeId { get; set; }

        public RemoveSeatType() { }
        public RemoveSeatType(Guid conferenceId) : base(conferenceId) { }
    }
}
