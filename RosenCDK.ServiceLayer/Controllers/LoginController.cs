using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Abp.Web.Models;
using Abp.WebApi.Controllers;
using RosenCDK.BussinessLogics;
using RosenCDK.DTO;
using RosenCDK.ServiceLayer.Authorizations;
using ServiceLayer.Authorization;
namespace RosenCDK.ServiceLayer.Controllers
{
    /// <summary>
    /// LoginController provides APIs to login
    /// </summary>
    [RoutePrefix("api/login")]
    public class LoginController : AbpApiController
    {
        public IPersonAppService _personAppService { get; set; }

        /// <summary>Logins the application</summary>
        /// <param name="loginMessageInput">The Login Input</param>
        /// <returns>The result of login process</returns>
        [SkipFilter]
        [HttpPost, Route("")]
        public LoginMessageOutputDTO Login(LoginMessageInputDTO loginMessageInput)
        {
            return _personAppService.CheckLoginCredential(loginMessageInput);
        }
    }
}