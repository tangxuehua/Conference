using System.Linq;
using System.Threading.Tasks;
using ConferenceManagement.Commands;
using ConferenceManagement.Messages;
using ECommon.Components;
using ECommon.IO;
using ENode.Commanding;
using ENode.Infrastructure;
using Payments.Messages;
using Registration.Commands.Orders;
using Registration.Orders;

namespace Registration.ProcessManagers
{
    [Component]
    public class RegistrationProcessManager :
        IMessageHandler<OrderPlaced>,
        IMessageHandler<OrderPaymentConfirmed>,
        IMessageHandler<OrderExpired>,
        IMessageHandler<SeatsReservedMessage>,
        IMessageHandler<SeatInsufficientMessage>,
        IMessageHandler<PaymentCompletedMessage>,
        IMessageHandler<PaymentRejectedMessage>
    {
        private ICommandService _commandService;

        public RegistrationProcessManager(ICommandService commandService)
        {
            _commandService = commandService;
        }

        public Task<AsyncTaskResult> HandleAsync(OrderPlaced evnt)
        {
            return _commandService.SendAsync(new MakeSeatReservation(evnt.ConferenceId)
            {
                ReservationId = evnt.AggregateRootId,
                Seats = evnt.OrderTotal.Lines.Select(x => new SeatReservationItemInfo { SeatType = x.SeatQuantity.Seat.SeatTypeId, Quantity = x.SeatQuantity.Quantity }).ToList()
            });
        }
        public Task<AsyncTaskResult> HandleAsync(SeatsReservedMessage message)
        {
            return _commandService.SendAsync(new ConfirmReservation(message.ReservationId, true));
        }
        public Task<AsyncTaskResult> HandleAsync(SeatInsufficientMessage message)
        {
            return _commandService.SendAsync(new ConfirmReservation(message.ReservationId, false));
        }
        public Task<AsyncTaskResult> HandleAsync(PaymentCompletedMessage message)
        {
            return _commandService.SendAsync(new ConfirmPayment(message.OrderId, true));
        }
        public Task<AsyncTaskResult> HandleAsync(PaymentRejectedMessage message)
        {
            return _commandService.SendAsync(new ConfirmPayment(message.OrderId, false));
        }
        public Task<AsyncTaskResult> HandleAsync(OrderPaymentConfirmed evnt)
        {
            if (evnt.OrderStatus == OrderStatus.PaymentSuccess)
            {
                return _commandService.SendAsync(new CommitSeatReservation(evnt.ConferenceId, evnt.AggregateRootId));
            }
            else if (evnt.OrderStatus == OrderStatus.PaymentRejected)
            {
                return _commandService.SendAsync(new CancelSeatReservation(evnt.ConferenceId, evnt.AggregateRootId));
            }
            return Task.FromResult(AsyncTaskResult.Success);
        }
        public Task<AsyncTaskResult> HandleAsync(SeatsReservationCommittedMessage message)
        {
            return _commandService.SendAsync(new MarkAsSuccess(message.ReservationId));
        }
        public Task<AsyncTaskResult> HandleAsync(SeatsReservationCancelledMessage message)
        {
            return _commandService.SendAsync(new CloseOrder(message.ReservationId));
        }
        public Task<AsyncTaskResult> HandleAsync(OrderExpired evnt)
        {
            return _commandService.SendAsync(new CancelSeatReservation(evnt.ConferenceId, evnt.AggregateRootId));
        }
    }
}
