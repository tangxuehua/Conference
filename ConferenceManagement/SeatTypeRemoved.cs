using System;
using ENode.Eventing;

namespace ConferenceManagement
{
    [Serializable]
    public class SeatTypeRemoved : DomainEvent<Guid>
    {
        public Guid SeatTypeId { get; private set; }

        public SeatTypeRemoved(Guid conferenceId, Guid seatTypeId) : base(conferenceId)
        {
            SeatTypeId = seatTypeId;
        }
    }
}
