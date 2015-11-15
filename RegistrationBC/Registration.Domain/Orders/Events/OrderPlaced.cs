using System;
using System.Collections.Generic;
using ENode.Eventing;
using ENode.Infrastructure;

namespace Registration.Orders
{
    [Serializable]
    [Code(3300)]
    public class OrderPlaced : DomainEvent<Guid>
    {
        public Guid ConferenceId { get; private set; }
        public OrderTotal OrderTotal { get; private set; }
        public DateTime ReservationExpirationDate { get; private set; }
        public string AccessCode { get; private set; }

        public OrderPlaced() { }
        public OrderPlaced(Order order, Guid conferenceId, OrderTotal orderTotal, DateTime reservationExpirationDate, string accessCode)
            : base(order)
        {
            ConferenceId = conferenceId;
            OrderTotal = orderTotal;
            ReservationExpirationDate = reservationExpirationDate;
            AccessCode = accessCode;
        }
    }
}
