using System;
using ENode.Eventing;

namespace ConferenceManagement
{
    [Serializable]
    public abstract class ConferenceEvent : DomainEvent<Guid>
    {
        public ConferenceInfo Info { get; private set; }

        public ConferenceEvent() { }
        public ConferenceEvent(Conference conference, ConferenceInfo info)
            : base(conference)
        {
            Info = info;
        }
    }
}
