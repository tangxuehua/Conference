using System;
using ENode.Commanding;
using ENode.Infrastructure;

namespace ConferenceManagement.Commands
{
    [Serializable]
    [Code(1108)]
    public class UpdateConference : Command<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Tagline { get; set; }
        public string TwitterSearch { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
