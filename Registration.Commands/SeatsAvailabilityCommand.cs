using System;
using ENode.Commanding;

namespace Registration.Commands
{
    [Serializable]
    public abstract class SeatsAvailabilityCommand : AggregateCommand<Guid>
    {
        public SeatsAvailabilityCommand(Guid conferenceId) : base(conferenceId) { }
    }
}
