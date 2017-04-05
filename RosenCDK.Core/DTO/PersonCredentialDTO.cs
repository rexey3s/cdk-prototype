using System.Collections.Generic;

namespace RosenCDK.DTO
{
    public class PersonCredentialDTO
    {
        public PersonCredentialDTO()
        {
            
        }
        public PersonCredentialDTO(string name, string authToken, List<string> activities)
        {
            Name = name;
            AuthToken = authToken;
            Activities = activities;
        }

        public string Name { get; set; }
        public string AuthToken { get; set; }
        public List<string> Activities { get; set; }
    }
}
