using ECommon.Components;
using ENode.Eventing;
using ENode.Infrastructure.Impl;
using Payments.EventHandlers;
using Payments.ReadModel;

namespace Conference.Web.Public.Providers
{
    [Component]
    public class EventHandlerTypeCodeProvider : DefaultTypeCodeProvider<IEventHandler>
    {
        public EventHandlerTypeCodeProvider()
        {
            //RegisterType<RegistrationProcessManager>(100);
            //RegisterType<DraftOrderViewModelGenerator>(101);
            //RegisterType<PricedOrderViewModelGenerator>(102);
            //RegisterType<SeatAssignmentsViewModelGenerator>(103);
            RegisterType<PaymentMessagePublisher>(104);
            RegisterType<PaymentViewModelGenerator>(105);
        }
    }
}
