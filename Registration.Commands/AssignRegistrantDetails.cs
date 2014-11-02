using System;
using ENode.Commanding;

namespace Registration.Commands
{
    public class AssignRegistrantDetails : AggregateCommand<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public AssignRegistrantDetails(Guid orderId) : base(orderId) { }
    }
}
