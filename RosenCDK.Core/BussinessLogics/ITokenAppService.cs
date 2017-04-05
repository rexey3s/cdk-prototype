using Abp.Application.Services;
using RosenCDK.DTO;

namespace RosenCDK.BussinessLogics
{
    public interface ITokenAppService : IApplicationService
    {
        /// <summary>
        ///  Function to generate unique token with expiry against the provided username.
        ///  Also add a record in database for generated token.
        /// </summary>
        /// <param name="username">A string that represent the username of user</param>
        /// <returns>A UserTokenDTO object that represent token information</returns>
        UserTokenDTO CheckUserTokenByUsername(string username);

        /// <summary>
        /// Method to validate token against expiry and existence in database.
        /// </summary>
        /// <param name="authToken">A string that represent the author token key of user</param>
        /// <returns>True if the token key is valid/False if the token is expires or invalid</returns>
        ResponseMessageDTO ValidateToken(string authToken);

        /// <summary>
        /// Method to kill the provided token id by username of user.
        /// </summary>
        /// <param name="username">A string that represent the username of user</param>
        /// <returns>True for successful delete/False if an error occur</returns>
        ResponseMessageDTO Kill(string username);

        /// <summary>
        ///  Function to get username with the provided token.
        /// </summary>
        /// <param name="authToken">A string that represent the token of user</param>
        /// <returns>A string that represent username</returns>
        string GetUsernameByToken(string authToken);
    }
}
