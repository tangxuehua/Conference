using System;
using ENode.Eventing;

namespace Conference.Contracts
{
    public class ConferenceCreated : DomainEvent<Guid>
    {
        public ConferenceCreated(Guid conferenceId) : base(conferenceId) { }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Slug { get; set; }
        public string Tagline { get; set; }
        public string TwitterSearch { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Owner Owner { get; set; }
    }
}
