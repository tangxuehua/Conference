using System;
using ENode.Commanding;

namespace ConferenceManagement.Commands
{
    [Serializable]
    public class CancelSeatReservation : AggregateCommand<Guid>
    {
        public Guid ReservationId { get; set; }

        public CancelSeatReservation(Guid conferenceId) : base(conferenceId) { }
    }
}
