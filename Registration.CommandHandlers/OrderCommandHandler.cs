using System.Linq;
using ECommon.Components;
using ENode.Commanding;
using Registration.Commands.Orders;
using Registration.Orders;

namespace Registration.CommandHandlers
{
    [Component]
    public class OrderCommandHandler :
        ICommandHandler<PlaceOrder>,
        ICommandHandler<ConfirmReservation>,
        ICommandHandler<ConfirmPayment>,
        ICommandHandler<MarkAsSuccess>,
        ICommandHandler<CloseOrder>
    {
        private readonly IPricingService _pricingService;

        public OrderCommandHandler(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }

        public void Handle(ICommandContext context, PlaceOrder command)
        {
            context.Add(new Order(
                command.AggregateRootId, 
                command.ConferenceId, 
                command.Seats.Select(t => new SeatQuantity(t.SeatType, t.Quantity)).ToList(),
                new Registrant(command.Registrant.FirstName, command.Registrant.LastName, command.Registrant.Email),
                _pricingService));
        }
        public void Handle(ICommandContext context, ConfirmReservation command)
        {
            context.Get<Order>(command.AggregateRootId).ConfirmReservation(command.IsReservationSuccess);
        }
        public void Handle(ICommandContext context, ConfirmPayment command)
        {
            context.Get<Order>(command.AggregateRootId).ConfirmPayment(command.IsPaymentSuccess);
        }
        public void Handle(ICommandContext context, MarkAsSuccess command)
        {
            context.Get<Order>(command.AggregateRootId).MarkAsSuccess();
        }
        public void Handle(ICommandContext context, CloseOrder command)
        {
            context.Get<Order>(command.AggregateRootId).Close();
        }
    }
}
