using System;

namespace RosenCDK.DTO
{
    public class UserTokenDTO 
    {
        public UserTokenDTO()
        {
            
        }
        public UserTokenDTO(int tokenId, string username, string authToken, DateTime? issuedOn, DateTime? expiresOn)
        {
            TokenId = tokenId;
            Username = username;
            AuthToken = authToken;
            IssuedOn = issuedOn;
            ExpiresOn = expiresOn;
        }

        public int TokenId { get; set; }
        public string Username { get; set; }
        public string AuthToken { get; set; }
        public DateTime? IssuedOn { get; set; }
        public DateTime? ExpiresOn { get; set; }
    }
}
