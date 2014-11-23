using System.Linq;
using ConferenceManagement.Commands;
using ConferenceManagement.Messages;
using ECommon.Components;
using ENode.Eventing;
using ENode.Infrastructure;
using ENode.Messaging;
using Payments.Messages;
using Registration.Commands.Orders;
using Registration.Orders;

namespace Registration.ProcessManagers
{
    [Component]
    public class RegistrationProcessManager :
        IEventHandler<OrderPlaced>,
        IEventHandler<OrderPaymentConfirmed>,
        IEventHandler<OrderExpired>,
        IMessageHandler<SeatsReservedMessage>,
        IMessageHandler<SeatInsufficientMessage>,
        IMessageHandler<PaymentCompletedMessage>,
        IMessageHandler<PaymentRejectedMessage>
    {
        public void Handle(IHandlingContext context, OrderPlaced evnt)
        {
            context.AddCommand(new MakeSeatReservation(evnt.ConferenceId)
            {
                ReservationId = evnt.AggregateRootId,
                Seats = evnt.OrderTotal.Lines.Select(x => new SeatReservationItemInfo { SeatType = x.SeatInfo.SeatTypeId, Quantity = x.SeatInfo.Quantity }).ToList()
            });
        }
        public void Handle(IHandlingContext context, SeatsReservedMessage message)
        {
            context.AddCommand(new ConfirmReservation(message.ReservationId, true));
        }
        public void Handle(IHandlingContext context, SeatInsufficientMessage message)
        {
            context.AddCommand(new ConfirmReservation(message.ReservationId, false));
        }
        public void Handle(IHandlingContext context, PaymentCompletedMessage message)
        {
            context.AddCommand(new ConfirmPayment(message.OrderId, true));
        }
        public void Handle(IHandlingContext context, PaymentRejectedMessage message)
        {
            context.AddCommand(new ConfirmPayment(message.OrderId, false));
        }
        public void Handle(IHandlingContext context, OrderPaymentConfirmed evnt)
        {
            if (evnt.IsPaymentSuccess)
            {
                context.AddCommand(new CommitSeatReservation(evnt.ConferenceId, evnt.AggregateRootId));
            }
            else
            {
                context.AddCommand(new CancelSeatReservation(evnt.ConferenceId, evnt.AggregateRootId));
            }
        }
        public void Handle(IHandlingContext context, SeatsReservationCommittedMessage message)
        {
            context.AddCommand(new MarkAsSuccess(message.ReservationId));
        }
        public void Handle(IHandlingContext context, SeatsReservationCancelledMessage message)
        {
            context.AddCommand(new CloseOrder(message.ReservationId));
        }
        public void Handle(IHandlingContext context, OrderExpired evnt)
        {
            context.AddCommand(new CancelSeatReservation(evnt.ConferenceId, evnt.AggregateRootId));
        }
    }
}
