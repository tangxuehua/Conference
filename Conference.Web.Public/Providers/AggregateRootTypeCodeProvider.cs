using ECommon.Components;
using ENode.Domain;
using ENode.Infrastructure.Impl;
using Payments;
using Registration.Orders;
using Registration.SeatAssigning;

namespace Conference.Web.Public.Providers
{
    [Component]
    public class AggregateRootTypeCodeProvider : DefaultTypeCodeProvider<IAggregateRoot>
    {
        public AggregateRootTypeCodeProvider()
        {
            RegisterType<Order>(100);
            RegisterType<SeatAssignments>(101);
            RegisterType<Payment>(102);
        }
    }
}
