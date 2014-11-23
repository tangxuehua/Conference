namespace Registration.ReadModel
{
    using System;

    public class ConferenceAlias
    {
        public Guid Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Tagline { get; set; }
    }
}
