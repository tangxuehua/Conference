using System;

namespace ConferenceManagement
{
    [Serializable]
    public class ConferenceUpdated : ConferenceEvent
    {
        public ConferenceUpdated(Guid id, ConferenceInfo info) : base(id, info)
        {
        }
    }
}
