using ECommon.Components;
using ENode.EQueue;
using ENode.Eventing;
using Payments;
using Payments.Messages;
using Registration.Orders;
using Registration.SeatAssigning;

namespace Conference.Web.Public.Providers
{
    [Component]
    public class EventTopicProvider : AbstractTopicProvider<IEvent>
    {
        public EventTopicProvider()
        {
            RegisterTopic("RegistrationEventTopic",
                //typeof(OrderConfirmed),
                //typeof(OrderPartiallyReserved),
                typeof(OrderPlaced),
                typeof(OrderRegistrantAssigned),
                //typeof(OrderReservationCompleted),
                //typeof(OrderTotalsCalculated),
                typeof(SeatAssigned),
                typeof(SeatAssignmentsCreated),
                typeof(SeatUnassigned));
                //typeof(AvailableSeatsChanged),
                //typeof(SeatsAvailabilityCreated),
                //typeof(SeatsReservationCancelled),
                //typeof(SeatsReservationCommitted),
                //typeof(SeatsReserved));
            RegisterTopic("PaymentEventTopic",
                typeof(PaymentInitiated),
                typeof(PaymentCompleted),
                typeof(PaymentRejected),
                typeof(PaymentCompletedMessage),
                typeof(PaymentRejectedMessage));
        }
    }
}
