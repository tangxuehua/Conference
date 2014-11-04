namespace Conference
{
    using System;
    using Infrastructure.Messaging;

    /// <summary>
    /// Event published whenever a conference is made public.
    /// </summary>
    public class ConferencePublished : IEvent
    {
        public Guid SourceId { get; set; }
    }
}
