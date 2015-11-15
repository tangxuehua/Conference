using System;
using ENode.Commanding;
using ENode.Infrastructure;

namespace Registration.Commands.Orders
{
    [Serializable]
    [Code(3105)]
    public class AssignRegistrantDetails : Command<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public AssignRegistrantDetails() { }
        public AssignRegistrantDetails(Guid orderId) : base(orderId) { }
    }
}
