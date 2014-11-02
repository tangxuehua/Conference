using System.Linq;
using ECommon.Components;
using ENode.Commanding;
using Registration.Commands;

namespace Registration.CommandHandlers
{
    [Component]
    public class OrderCommandHandler :
        ICommandHandler<RegisterToConference>,
        ICommandHandler<MarkSeatsAsReserved>,
        ICommandHandler<AssignRegistrantDetails>,
        ICommandHandler<ConfirmOrder>
    {
        private readonly IPricingService pricingService;

        public OrderCommandHandler(IPricingService pricingService)
        {
            this.pricingService = pricingService;
        }

        public void Handle(ICommandContext context, RegisterToConference command)
        {
            var items = command.Seats.Select(t => new OrderItem(t.SeatType, t.Quantity)).ToList();
            context.Add(new Order(command.AggregateRootId, command.ConferenceId, items, pricingService));
        }

        public void Handle(ICommandContext context, MarkSeatsAsReserved command)
        {
            context.Get<Order>(command.AggregateRootId).MarkAsReserved(pricingService, command.Seats);
        }

        public void Handle(ICommandContext context, AssignRegistrantDetails command)
        {
            context.Get<Order>(command.AggregateRootId).AssignRegistrant(command.FirstName, command.LastName, command.Email);
        }

        public void Handle(ICommandContext context, ConfirmOrder command)
        {
            context.Get<Order>(command.AggregateRootId).Confirm();
        }
    }
}
