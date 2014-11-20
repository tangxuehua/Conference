using System.Linq;
using ECommon.Components;
using ENode.Commanding;
using Registration.Commands;
using Registration.Orders;

namespace Registration.CommandHandlers
{
    [Component]
    public class OrderCommandHandler :
        ICommandHandler<PlaceOrder>,
        ICommandHandler<ConfirmReservation>,
        ICommandHandler<AssignRegistrantDetails>,
        ICommandHandler<ConfirmPayment>
    {
        private readonly IPricingService _pricingService;

        public OrderCommandHandler(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }

        public void Handle(ICommandContext context, PlaceOrder command)
        {
            context.Add(new Order(command.AggregateRootId, command.ConferenceId, command.Seats.Select(t => new SeatQuantity(t.SeatType, t.Quantity)).ToList(), _pricingService));
        }
        public void Handle(ICommandContext context, ConfirmReservation command)
        {
            context.Get<Order>(command.AggregateRootId).ConfirmReservation();
        }
        public void Handle(ICommandContext context, AssignRegistrantDetails command)
        {
            context.Get<Order>(command.AggregateRootId).AssignRegistrant(new RegistrantInfo(command.FirstName, command.LastName, command.Email));
        }
        public void Handle(ICommandContext context, ConfirmPayment command)
        {
            context.Get<Order>(command.AggregateRootId).ConfirmPayment();
        }
    }
}
