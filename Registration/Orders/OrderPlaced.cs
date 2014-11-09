using System;
using System.Collections.Generic;
using ENode.Eventing;

namespace Registration.Orders
{
    public class OrderPlaced : DomainEvent<Guid>
    {
        public Guid ConferenceId { get; private set; }
        public IEnumerable<SeatQuantity> Seats { get; private set; }
        public string AccessCode { get; private set; }

        public OrderPlaced(Guid sourceId, Guid conferenceId, IEnumerable<SeatQuantity> seats, string accessCode) : base(sourceId)
        {
            ConferenceId = conferenceId;
            Seats = seats;
            AccessCode = accessCode;
        }
    }
}
