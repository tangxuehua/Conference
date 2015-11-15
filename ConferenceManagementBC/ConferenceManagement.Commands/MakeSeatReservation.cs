using System;
using System.Collections.Generic;
using ENode.Commanding;
using ENode.Infrastructure;

namespace ConferenceManagement.Commands
{
    [Serializable]
    [Code(1104)]
    public class MakeSeatReservation : Command<Guid>
    {
        public Guid ReservationId { get; set; }
        public IEnumerable<SeatReservationItemInfo> Seats { get; set; }

        public MakeSeatReservation() { }
        public MakeSeatReservation(Guid conferenceId) : base(conferenceId)
        {
            this.Seats = new List<SeatReservationItemInfo>();
        }
    }
}
