using System.Linq;
using System.Threading.Tasks;
using ConferenceManagement.Commands;
using ConferenceManagement.Messages;
using ECommon.Components;
using ENode.Commanding;
using ENode.Messaging;
using Payments.Messages;
using Registration.Commands.Orders;
using Registration.Commands.SeatAssignments;
using Registration.Orders;

namespace Registration.ProcessManagers
{
    [Component]
    public class RegistrationProcessManager :
        IMessageHandler<OrderPlaced>,                           //订单创建时发生(Order)

        IMessageHandler<SeatsReservedMessage>,                  //预扣库存，成功时发生(Conference)
        IMessageHandler<SeatInsufficientMessage>,               //预扣库存，库存不足时发生(Conference)

        IMessageHandler<PaymentCompletedMessage>,               //支付成功时发生(Payment)
        IMessageHandler<PaymentRejectedMessage>,                //支付拒绝时发生(Payment)

        IMessageHandler<OrderPaymentConfirmed>,                 //确认支付时发生(Order)

        IMessageHandler<SeatsReservationCommittedMessage>,      //预扣库存提交时发生(Conference)
        IMessageHandler<SeatsReservationCancelledMessage>,      //预扣库存取消时发生(Conference)

        IMessageHandler<OrderSuccessed>,                        //订单处理成功时发生(Order)

        IMessageHandler<OrderExpired>                           //订单过期时(15分钟过期)发生(Order)
    {
        private ICommandService _commandService;

        public RegistrationProcessManager(ICommandService commandService)
        {
            _commandService = commandService;
        }

        public Task HandleAsync(OrderPlaced evnt)
        {
            return _commandService.SendAsync(new MakeSeatReservation(evnt.ConferenceId)
            {
                ReservationId = evnt.AggregateRootId,
                Seats = evnt.OrderTotal.Lines.Select(x => new SeatReservationItemInfo { SeatType = x.SeatQuantity.Seat.SeatTypeId, Quantity = x.SeatQuantity.Quantity }).ToList()
            });
        }

        public Task HandleAsync(SeatsReservedMessage message)
        {
            return _commandService.SendAsync(new ConfirmReservation(message.ReservationId, true));
        }
        public Task HandleAsync(SeatInsufficientMessage message)
        {
            return _commandService.SendAsync(new ConfirmReservation(message.ReservationId, false));
        }

        public Task HandleAsync(PaymentCompletedMessage message)
        {
            return _commandService.SendAsync(new ConfirmPayment(message.OrderId, true));
        }
        public Task HandleAsync(PaymentRejectedMessage message)
        {
            return _commandService.SendAsync(new ConfirmPayment(message.OrderId, false));
        }

        public async Task HandleAsync(OrderPaymentConfirmed evnt)
        {
            if (evnt.OrderStatus == OrderStatus.PaymentSuccess)
            {
                await _commandService.SendAsync(new CommitSeatReservation(evnt.ConferenceId, evnt.AggregateRootId));
            }
            else if (evnt.OrderStatus == OrderStatus.PaymentRejected)
            {
                await _commandService.SendAsync(new CancelSeatReservation(evnt.ConferenceId, evnt.AggregateRootId));
            }
        }

        public Task HandleAsync(SeatsReservationCommittedMessage message)
        {
            return _commandService.SendAsync(new MarkAsSuccess(message.ReservationId));
        }
        public Task HandleAsync(SeatsReservationCancelledMessage message)
        {
            return _commandService.SendAsync(new CloseOrder(message.ReservationId));
        }

        public Task HandleAsync(OrderSuccessed evnt)
        {
            return _commandService.SendAsync(new CreateSeatAssignments(evnt.AggregateRootId));
        }

        public Task HandleAsync(OrderExpired evnt)
        {
            return _commandService.SendAsync(new CancelSeatReservation(evnt.ConferenceId, evnt.AggregateRootId));
        }
    }
}
