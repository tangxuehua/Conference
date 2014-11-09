using System;
using ENode.Eventing;

namespace Registration.Events
{
    public class OrderRegistrantAssigned : DomainEvent<Guid>
    {
        public RegistrantInfo Registrant { get; private set; }

        public OrderRegistrantAssigned(Guid sourceId, RegistrantInfo registrant) : base(sourceId)
        {
            Registrant = registrant;
        }
    }
}
