using ECommon.Components;
using ENode.Domain;
using ENode.Infrastructure.Impl;
using Payments;
using Registration.Orders;
using Registration.SeatAssigning;
using Registration.SeatAvailabilities;

namespace Conference.Web.Public.Providers
{
    [Component]
    public class AggregateRootTypeCodeProvider : DefaultTypeCodeProvider<IAggregateRoot>
    {
        public AggregateRootTypeCodeProvider()
        {
            RegisterType<Order>(100);
            RegisterType<SeatsAvailability>(101);
            RegisterType<SeatAssignments>(102);
            RegisterType<Payment>(103);
        }
    }
}
