namespace Registration.ReadModel
{
    using System;

    public class ConferenceDetails
    {
        public Guid Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Tagline { get; set; }
        public string TwitterSearch { get; set; }
        public DateTimeOffset StartDate { get; set; }
    }
}
