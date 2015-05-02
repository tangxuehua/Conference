using System;
using ENode.Commanding;

namespace Registration.Commands.Orders
{
    [Serializable]
    public class AssignRegistrantDetails : Command<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public AssignRegistrantDetails(Guid orderId) : base(orderId) { }
    }
}
