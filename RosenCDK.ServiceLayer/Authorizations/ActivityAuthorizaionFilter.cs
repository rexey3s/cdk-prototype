using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using JetBrains.Annotations;
using RosenCDK.BussinessLogics;
using RosenCDK.DTO;
using RosenCDK.Entities;
using RosenCDK.ServiceLayer.Authorizations;
using ServiceLayer.Authorization;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;
using IAuthorizationFilter = System.Web.Http.Filters.IAuthorizationFilter;

namespace RosenCDK.Authorizations
{
    public class ActivityAuthorizationFilter : AuthorizationFilterAttribute
    {
        public ITokenAppService _tokenAppService { get; set; }
        public IPersonAppService _personAppService { get; set; }
        
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            HttpRequestMessage request = actionContext.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;
            if(actionContext.ActionDescriptor.GetCustomAttributes<SkipFilterAttribute>().Any()) return;
            
            ActivityAuthorize activityAuthorize = actionContext
                .ActionDescriptor.GetCustomAttributes<ActivityAuthorize>().SingleOrDefault();
            if (activityAuthorize == null || authorization == null)
            {
                actionContext.Response = request.CreateErrorResponse(HttpStatusCode.Unauthorized, 
                    _personAppService.Localize("Unauthorized_Request_NoUser"));
                return;
            }

            string userAuthToken = authorization.ToString();
            // Authentication by validate user token 
            ResponseMessageDTO tokenResponseMessageDto = _tokenAppService.ValidateToken(userAuthToken);
            if (!tokenResponseMessageDto.Status)
            {
                actionContext.Response = request.CreateErrorResponse(HttpStatusCode.Unauthorized, 
                    tokenResponseMessageDto.Message);
                return;
            }
            else
            {
                string username = _tokenAppService.GetUsernameByToken(userAuthToken);

                if (string.IsNullOrEmpty(activityAuthorize.ActivityName))
                {
                    return;
                }
                ResponseMessageDTO authorizeActivityResponseMessageDto = _personAppService
                    .CheckAuthorizeActivity(username, activityAuthorize.ActivityName);
                if (!authorizeActivityResponseMessageDto.Status)
                {
                    actionContext.Response = request.CreateErrorResponse(HttpStatusCode.Unauthorized,
                        authorizeActivityResponseMessageDto.Message);
                    return;
                }
            }
        }
    }

}
