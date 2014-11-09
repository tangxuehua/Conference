using ECommon.Components;
using ENode.EQueue;
using ENode.Eventing;
using Payments;
using Payments.Contracts;
using Registration.Orders;
using Registration.SeatAssigning;
using Registration.SeatAvailabilities;

namespace Conference.Web.Public.Providers
{
    [Component]
    public class EventTopicProvider : AbstractTopicProvider<IEvent>
    {
        public EventTopicProvider()
        {
            RegisterTopic("RegistrationEventTopic",
                typeof(OrderConfirmed),
                typeof(OrderPartiallyReserved),
                typeof(OrderPlaced),
                typeof(OrderRegistrantAssigned),
                typeof(OrderReservationCompleted),
                typeof(OrderTotalsCalculated),
                typeof(SeatAssigned),
                typeof(SeatAssignmentsCreated),
                typeof(SeatUnassigned),
                typeof(AvailableSeatsChanged),
                typeof(SeatsAvailabilityCreated),
                typeof(SeatsReservationCancelled),
                typeof(SeatsReservationCommitted),
                typeof(SeatsReserved));
            RegisterTopic("PaymentEventTopic",
                typeof(PaymentInitiated),
                typeof(PaymentCompleted),
                typeof(PaymentRejected),
                typeof(PaymentInitiatedEvent),
                typeof(PaymentCompletedEvent),
                typeof(PaymentRejectedEvent));
        }
    }
}
