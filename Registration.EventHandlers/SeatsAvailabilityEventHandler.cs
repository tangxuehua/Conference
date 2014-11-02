using Conference.Contracts;
using ENode.Eventing;
using Registration.Commands;

namespace Registration.EventHandlers
{
    public class SeatsAvailabilityEventHandler : IEventHandler<ConferenceCreated>
    {
        public void Handle(IEventContext context, ConferenceCreated evnt)
        {
            context.AddCommand(new CreateSeatsAvailability(evnt.AggregateRootId));
        }
    }
}
