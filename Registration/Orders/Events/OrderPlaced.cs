using System;
using System.Collections.Generic;
using ENode.Eventing;

namespace Registration.Orders
{
    [Serializable]
    public class OrderPlaced : DomainEvent<Guid>
    {
        public Guid ConferenceId { get; private set; }
        public OrderTotal OrderTotal { get; private set; }
        public Registrant Registrant { get; private set; }
        public DateTime ReservationAutoExpiration { get; private set; }
        public string AccessCode { get; private set; }

        public OrderPlaced(Order order, Guid conferenceId, OrderTotal orderTotal, Registrant registrant, DateTime reservationAutoExpiration, string accessCode)
            : base(order)
        {
            ConferenceId = conferenceId;
            OrderTotal = orderTotal;
            Registrant = registrant;
            ReservationAutoExpiration = reservationAutoExpiration;
            AccessCode = accessCode;
        }
    }
}
