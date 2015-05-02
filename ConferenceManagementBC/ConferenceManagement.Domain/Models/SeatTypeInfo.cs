using System;

namespace ConferenceManagement
{
    [Serializable]
    public class SeatTypeInfo
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }

        public SeatTypeInfo(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
    }
}
