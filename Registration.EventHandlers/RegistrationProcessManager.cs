using System.Linq;
using ECommon.Components;
using ENode.Eventing;
using Payments.Contracts;
using Registration.Commands;
using Registration.Orders;
using Registration.SeatAvailabilities;

namespace Registration.EventHandlers
{
    [Component]
    public class RegistrationProcessManager :
        IEventHandler<OrderPlaced>,
        IEventHandler<SeatsReserved>,
        IEventHandler<PaymentCompletedEvent>,
        IEventHandler<OrderConfirmed>
    {
        public void Handle(IEventContext context, OrderPlaced evnt)
        {
            context.AddCommand(new MakeSeatReservation(evnt.ConferenceId)
            {
                ReservationId = evnt.AggregateRootId,
                Seats = evnt.Seats.Select(x => new SeatInfo { SeatType = x.SeatType, Quantity = x.Quantity }).ToList()
            });
        }
        public void Handle(IEventContext context, SeatsReserved evnt)
        {
            context.AddCommand(new MarkSeatsAsReserved(evnt.OrderId)
            {
                Seats = evnt.ReservationDetails.Select(x => new SeatInfo { SeatType = x.SeatType, Quantity = x.Quantity }).ToList()
            });
        }
        public void Handle(IEventContext context, PaymentCompletedEvent evnt)
        {
            context.AddCommand(new ConfirmOrder(evnt.AggregateRootId));
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
