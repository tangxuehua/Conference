using System;

namespace ConferenceManagement
{
    [Serializable]
    public class ConferenceUpdated : ConferenceEvent
    {
        public ConferenceUpdated(Conference conference, ConferenceInfo info)
            : base(conference, info)
        {
        }
    }
}
