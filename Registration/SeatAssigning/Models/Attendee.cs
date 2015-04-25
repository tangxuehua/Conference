using System;
using ECommon.Utilities;

namespace Registration.SeatAssigning
{
    [Serializable]
    public class Attendee
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }

        public Attendee() { }
        public Attendee(string firstName, string lastName, string email)
        {
            Ensure.NotNull(firstName, "firstName");
            Ensure.NotNull(lastName, "lastName");
            Ensure.NotNull(email, "email");
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
