using System;
using ENode.Commanding;

namespace Registration.Commands
{
    public class CreateSeatsAvailability : SeatsAvailabilityCommand, ICreatingAggregateCommand
    {
        public CreateSeatsAvailability(Guid conferenceId) : base(conferenceId) { }
    }
}
