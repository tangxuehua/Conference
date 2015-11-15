using System;
using ENode.Infrastructure;

namespace ConferenceManagement
{
    [Serializable]
    [Code(1300)]
    public class ConferenceCreated : ConferenceEvent
    {
        public ConferenceCreated() { }
        public ConferenceCreated(Conference conference, ConferenceInfo info)
            : base(conference, info)
        {
        }
    }
}
