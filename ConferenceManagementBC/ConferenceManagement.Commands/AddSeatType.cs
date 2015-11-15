using System;
using ENode.Commanding;
using ENode.Infrastructure;

namespace ConferenceManagement.Commands
{
    [Serializable]
    [Code(1100)]
    public class AddSeatType : Command<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public AddSeatType() { }
        public AddSeatType(Guid conferenceId) : base(conferenceId) { }
    }
}
