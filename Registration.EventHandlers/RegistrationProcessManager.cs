using System.Linq;
using ECommon.Components;
using ENode.Eventing;
using Payments.Contracts.Events;
using Registration.Commands;
using Registration.Events;

namespace Registration.EventHandlers
{
    [Component(LifeStyle.Singleton)]
    public class RegistrationProcessManager :
        IEventHandler<OrderPlaced>,
        IEventHandler<OrderUpdated>,
        IEventHandler<SeatsReserved>,
        IEventHandler<PaymentCompleted>,
        IEventHandler<OrderConfirmed>
    {
        public void Handle(IEventContext context, OrderPlaced evnt)
        {
            context.AddCommand(new MakeSeatReservation(evnt.ConferenceId)
            {
                ReservationId = evnt.AggregateRootId,
                Seats = evnt.Seats.ToList()
            });
        }
        public void Handle(IEventContext context, OrderUpdated evnt)
        {
            context.AddCommand(new MakeSeatReservation(evnt.ConferenceId)
            {
                ReservationId = evnt.AggregateRootId,
                Seats = evnt.Seats.ToList()
            });
        }
        public void Handle(IEventContext context, SeatsReserved evnt)
        {
            context.AddCommand(new MarkSeatsAsReserved(evnt.AggregateRootId)
            {
                Seats = evnt.ReservationDetails.ToList()
            });
        }
        public void Handle(IEventContext context, PaymentCompleted evnt)
        {
            context.AddCommand(new ConfirmOrder(evnt.SourceOrderId));
        }
        public void Handle(IEventContext context, OrderConfirmed evnt)
        {
            context.AddCommand(new CommitSeatReservation(evnt.ConferenceId)
            {
                ReservationId = evnt.AggregateRootId
            });
            context.AddCommand(new CreateSeatAssignments(evnt.AggregateRootId));
        }
    }
}
