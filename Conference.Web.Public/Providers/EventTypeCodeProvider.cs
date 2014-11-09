using ECommon.Components;
using ENode.Eventing;
using ENode.Infrastructure.Impl;
using Payments;
using Payments.Contracts;
using Registration.Orders;
using Registration.SeatAssigning;
using Registration.SeatAvailabilities;

namespace Conference.Web.Public.Providers
{
    [Component]
    public class EventTypeCodeProvider : DefaultTypeCodeProvider<IEvent>
    {
        public EventTypeCodeProvider()
        {
            RegisterType<OrderConfirmed>(100);
            RegisterType<OrderPartiallyReserved>(101);
            RegisterType<OrderPlaced>(102);
            RegisterType<OrderRegistrantAssigned>(103);
            RegisterType<OrderReservationCompleted>(104);
            RegisterType<OrderTotalsCalculated>(105);
            RegisterType<SeatAssigned>(106);
            RegisterType<SeatAssignmentsCreated>(107);
            RegisterType<SeatUnassigned>(108);
            RegisterType<AvailableSeatsChanged>(109);
            RegisterType<SeatsAvailabilityCreated>(110);
            RegisterType<SeatsReservationCancelled>(111);
            RegisterType<SeatsReservationCommitted>(112);
            RegisterType<SeatsReserved>(113);

            RegisterType<PaymentInitiated>(200);
            RegisterType<PaymentCompleted>(201);
            RegisterType<PaymentRejected>(202);
            RegisterType<PaymentInitiatedEvent>(203);
            RegisterType<PaymentCompletedEvent>(204);
            RegisterType<PaymentRejectedEvent>(205);
        }
    }
}
