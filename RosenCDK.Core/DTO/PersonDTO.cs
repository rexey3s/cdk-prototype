namespace RosenCDK.DTO
{
    public class PersonDTO 
    {
        public PersonDTO()
        {
            
        }
        public PersonDTO(int personId, string name, string company, string username, string password, int roleId)
        {
            PersonId = personId;
            Name = name;
            Company = company;
            Username = username;
            Password = password;
            RoleId = roleId;
        }

        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        
    }
}