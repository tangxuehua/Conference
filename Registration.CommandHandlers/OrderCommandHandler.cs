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
        private readonly IPricingService _pricingService;

        public OrderCommandHandler(IPricingService pricingService)
        {
            this._pricingService = pricingService;
        }

        public void Handle(ICommandContext context, RegisterToConference command)
        {
            context.Add(new Order(command.AggregateRootId, command.ConferenceId, command.Seats.Select(t => new SeatQuantity(t.SeatType, t.Quantity)), _pricingService));
        }
        public void Handle(ICommandContext context, MarkSeatsAsReserved command)
        {
            context.Get<Order>(command.AggregateRootId).MarkAsReserved(_pricingService, command.Seats.Select(x => new SeatQuantity(x.SeatType, x.Quantity)));
        }
        public void Handle(ICommandContext context, AssignRegistrantDetails command)
        {
            context.Get<Order>(command.AggregateRootId).AssignRegistrant(new RegistrantInfo(command.FirstName, command.LastName, command.Email));
        }
        public void Handle(ICommandContext context, ConfirmOrder command)
        {
            context.Get<Order>(command.AggregateRootId).Confirm();
        }
    }
}
