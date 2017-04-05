using Abp.Application.Services;
using RosenCDK.DTO;

namespace RosenCDK.BussinessLogics
{
    public interface IPersonAppService : IApplicationService
    {
        /// <summary>
        /// Check if the login inputs (username and passwrod) are correct 
        /// </summary>
        /// <param name="loginMessageInput">Username and Password</param>
        /// <returns>A LoginMessageOutputDTO object represent the status (true/false) of login process</returns>
        LoginMessageOutputDTO CheckLoginCredential(LoginMessageInputDTO loginMessageInput);

        /// <summary>
        /// Check if user have permisson to do a activity was given as parameter.
        /// The function also need a username as parameter to get user information
        /// </summary>
        /// <param name="activityName">Activity that you want to check</param>
        /// <param name="username">Username</param>
        /// <returns>A ResponseMessageDTO object represent the status (true/false) of checking authorize process</returns>
        ResponseMessageDTO CheckAuthorizeActivity(string username, string activityName);

        string Localize(string localizationPropertyName);
    }
}
