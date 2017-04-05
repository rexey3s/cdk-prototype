using System.ComponentModel.DataAnnotations;

namespace RosenCDK.DTO
{
    public class LoginMessageInputDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
