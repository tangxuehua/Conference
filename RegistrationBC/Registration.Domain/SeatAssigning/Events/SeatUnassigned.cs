using System;
using ENode.Eventing;
using ENode.Infrastructure;

namespace Registration.SeatAssigning
{
    [Serializable]
    [Code(3308)]
    public class SeatUnassigned : DomainEvent<Guid>
    {
        public int Position { get; private set; }

        public SeatUnassigned() { }
        public SeatUnassigned(OrderSeatAssignments source, int position) : base(source)
        {
            Position = position;
        }
    }
}
