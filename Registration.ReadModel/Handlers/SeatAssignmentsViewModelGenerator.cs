using ECommon.Components;
using ENode.Eventing;
using ENode.Infrastructure;
using Registration.SeatAssigning;

namespace Registration.Handlers
{
    [Component]
    public class SeatAssignmentsViewModelGenerator :
        IEventHandler<SeatAssignmentsCreated>,
        IEventHandler<SeatAssigned>,
        IEventHandler<SeatUnassigned>
    {
        public void Handle(IHandlingContext eventContext, SeatAssignmentsCreated evnt) { }
        public void Handle(IHandlingContext eventContext, SeatAssigned evnt) { }
        public void Handle(IHandlingContext eventContext, SeatUnassigned evnt) { }
    }
}
