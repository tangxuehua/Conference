using System.Linq;
using ConferenceManagement.Commands;
using ConferenceManagement.Messages;
using ECommon.Components;
using ENode.Eventing;
using ENode.Infrastructure;
using ENode.Messaging;
using Payments.Messages;
using Registration.Commands;
using Registration.Orders;

namespace Registration.ProcessManagers
{
    [Component]
    public class RegistrationProcessManager :
        IEventHandler<OrderPlaced>,
        IMessageHandler<SeatsReservedMessage>,
        IMessageHandler<PaymentCompletedMessage>,
        IEventHandler<OrderPaymentConfirmed>
    {
        public void Handle(IHandlingContext context, OrderPlaced evnt)
        {
            context.AddCommand(new MakeSeatReservation(evnt.ConferenceId)
            {
                ReservationId = evnt.AggregateRootId,
                Seats = evnt.Total.Lines.Select(x => new SeatReservationItemInfo { SeatType = x.SeatType, Quantity = x.Quantity }).ToList()
            });
        }
        public void Handle(IHandlingContext context, SeatsReservedMessage message)
        {
            context.AddCommand(new ConfirmReservation(message.ReservationId));
        }
        public void Handle(IHandlingContext context, PaymentCompletedMessage message)
        {
            context.AddCommand(new ConfirmPayment(message.OrderId));
        }
        public void Handle(IHandlingContext context, OrderPaymentConfirmed evnt)
        {
            context.AddCommand(new CommitSeatReservation(evnt.ConferenceId)
            {
                ReservationId = evnt.AggregateRootId
            });
            context.AddCommand(new CreateSeatAssignments(evnt.AggregateRootId));
        }
    }
}
