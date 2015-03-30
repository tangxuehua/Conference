using System;
using ENode.Commanding;

namespace ConferenceManagement.Commands
{
    [Serializable]
    public class AddSeatType : Command<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public AddSeatType(Guid conferenceId) : base(conferenceId) { }
    }
}
