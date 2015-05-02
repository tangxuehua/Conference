using System;

namespace Registration.Commands
{
    [Serializable]
    public class PersonalInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
