using System;
using System.Collections.Generic;
using ENode.Eventing;

namespace Registration.Events
{
    public class OrderPlaced : DomainEvent<Guid>
    {
        public Guid ConferenceId { get; set; }
        public IEnumerable<SeatQuantity> Seats { get; set; }
        public string AccessCode { get; set; }

        public OrderPlaced(Guid sourceId) : base(sourceId) { }
    }
}
