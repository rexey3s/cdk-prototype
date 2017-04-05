using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RosenCDK.DTO;
using RosenCDK.Repositories;
using RosenCDK.Entities;

namespace RosenCDK.BussinessLogics
{
    public class TokenAppService : RosenCDKAppServiceBase, ITokenAppService
    {
        private readonly IUserTokenRepository _userTokenRepository;

        public TokenAppService(IUserTokenRepository userTokenRepo)
        {
            _userTokenRepository = userTokenRepo;
        }

        public UserTokenDTO CheckUserTokenByUsername(string username)
        {
            
            //UserTokenDTO userTokenDto = GetToken(username);
            UserToken existedUserToken = _userTokenRepository.GetTokenByUsername(username);
            // Create new token unless exist
            if (existedUserToken == null)
            {
                string authToken = Guid.NewGuid().ToString();

                //The token will expires after 24 hours
                DateTime issuedOn = DateTime.Now;
                DateTime expiresOn = issuedOn.AddHours(24);

                //We will cal DAL method to insert there information into Token table
                UserToken userToken = _userTokenRepository.Insert(new UserToken()
                {
                    Username = username,
                    AuthToken = authToken,
                    IssuedOn = issuedOn,
                    ExpiresOn = expiresOn
                });
                return new UserTokenDTO(userToken.Id, userToken.Username,
                    userToken.AuthToken, userToken.IssuedOn, userToken.ExpiresOn);

            }
            else
            {
                existedUserToken.IssuedOn = DateTime.Now;
                existedUserToken.ExpiresOn = DateTime.Now.AddHours(24);
                return new UserTokenDTO(existedUserToken.Id, existedUserToken.Username,
                    existedUserToken.AuthToken, existedUserToken.IssuedOn, existedUserToken.ExpiresOn);
            }
          
        }



        public string GetUsernameByToken(string authToken)
        {
            //We will call the DAL method to get a Token from Token table by authToken
            UserToken userToken = _userTokenRepository.GetTokenByAuthToken(authToken);
            if (userToken == null)
            {
                return null;
            }
            return userToken.Username;
        }

        public ResponseMessageDTO Kill(string username)
        {
            //We will call DAL method to delete the token by username of user
            _userTokenRepository.Delete(userToken => userToken.Username == username);
            if (_userTokenRepository.GetTokenByUsername(username) != null)
            {
                return new ResponseMessageDTO(false, L("Delete_UserTokenFailed", username));
            }
            return new ResponseMessageDTO(true,
                L("Delete_UserTokenSuccess", username));
        }

        public ResponseMessageDTO ValidateToken(string authToken)
        { 
           
            //We will call the DAL method to get a Token from Token table by authToken
            UserToken userToken = _userTokenRepository.GetTokenByAuthToken(authToken);
            if (userToken == null || DateTime.Now > userToken.ExpiresOn)
            {
                return new ResponseMessageDTO(false, L("Invalid_UserToken"));
            }
            else
            {
                return new ResponseMessageDTO(true, L("Valid_UserToken"));
            }
        }
    }
}
