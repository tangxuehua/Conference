using System;
using ENode.Eventing;
using ENode.Infrastructure;

namespace Registration.Orders
{
    [Serializable]
    [Code(3309)]
    public class OrderRegistrantAssigned : DomainEvent<Guid>
    {
        public Guid ConferenceId { get; private set; }
        public Registrant Registrant { get; private set; }

        public OrderRegistrantAssigned() { }
        public OrderRegistrantAssigned(Order order, Guid conferenceId, Registrant registrant) : base(order)
        {
            ConferenceId = conferenceId;
            Registrant = registrant;
        }
    }
}
