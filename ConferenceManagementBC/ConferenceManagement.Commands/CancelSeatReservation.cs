using System;
using ENode.Commanding;
using ENode.Infrastructure;

namespace ConferenceManagement.Commands
{
    [Serializable]
    [Code(1101)]
    public class CancelSeatReservation : Command<Guid>
    {
        public Guid ReservationId { get; set; }

        public CancelSeatReservation() { }
        public CancelSeatReservation(Guid conferenceId, Guid reservationId) : base(conferenceId)
        {
            ReservationId = reservationId;
        }
    }
}
