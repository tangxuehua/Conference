using System;

namespace Registration.Orders
{
    [Serializable]
    public class OrderRegistrantAssigned : OrderEvent
    {
        public RegistrantInfo Registrant { get; private set; }

        public OrderRegistrantAssigned(Guid orderId, Guid conferenceId, RegistrantInfo registrant) : base(orderId, conferenceId)
        {
            Registrant = registrant;
        }
    }
}
