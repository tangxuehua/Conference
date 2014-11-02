using System;
using ENode.Eventing;

namespace Registration.Events
{
    public class OrderRegistrantAssigned : DomainEvent<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public OrderRegistrantAssigned(Guid sourceId) : base(sourceId) { }
    }
}
