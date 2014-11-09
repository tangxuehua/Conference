using ECommon.Components;
using ENode.Eventing;
using Registration.SeatAssigning;

namespace Registration.Handlers
{
    [Component]
    public class SeatAssignmentsViewModelGenerator :
        IEventHandler<SeatAssignmentsCreated>,
        IEventHandler<SeatAssigned>,
        IEventHandler<SeatUnassigned>
    {
        public void Handle(IEventContext eventContext, SeatAssignmentsCreated evnt) { }
        public void Handle(IEventContext eventContext, SeatAssigned evnt) { }
        public void Handle(IEventContext eventContext, SeatUnassigned evnt) { }
    }
}
