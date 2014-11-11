using System;
using ECommon.Utilities;

namespace Registration.Orders
{
    [Serializable]
    public class RegistrantInfo
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }

        public RegistrantInfo(string firstName, string lastName, string email)
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
