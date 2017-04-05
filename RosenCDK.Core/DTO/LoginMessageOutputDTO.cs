namespace RosenCDK.DTO
{
    public class LoginMessageOutputDTO
    {
        public LoginMessageOutputDTO(bool status, PersonCredentialDTO userData)
        {
            Status = status;
            UserData = userData;
        }

        public bool Status { get; set; }

        public PersonCredentialDTO UserData { get; set; }
    }
}
