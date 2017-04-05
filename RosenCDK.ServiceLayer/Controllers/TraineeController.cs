using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Abp.WebApi.Controllers;
using RosenCDK.Authorizations;
using RosenCDK.BussinessLogics;
using RosenCDK.DTO;
using RosenCDK.ServiceLayer.Authorizations;
using ServiceLayer.Authorization;

namespace RosenCDK.ServiceLayer.Controllers
{
    /// <summary>
    /// TraineeController provides APIs to work with Trainees data
    /// </summary>
    [RoutePrefix("api/trainee")]
    public class TraineeController : AbpApiController
    {
        public ITraineeAppService _traineeAppService { get; set; }


        /// <summary>Gets full Trainee information based on Token/summary>
        /// <returns>Full trainee information based on Token</returns>
        [Route("profile")]
        [ActivityAuthorize("")]
        public FullTraineeDetailDTO GetTraineeFullInfo()
        {
            var headers = Request.Headers;
            string userAuthToken = headers.Authorization.ToString();
            return _traineeAppService.GetFullTraineeInfo(userAuthToken);
        }

        /// <summary>Gets full trainee information based on id</summary>
        /// <param name="id">The trainee id.</param>
        /// <returns>Full trainee information based on id.</returns>
        [Route("info/{id:int}")]
        [ActivityAuthorize(ActivityAuthorize.ExecuteDeltaExecute)]
        public TraineeDTO GetTraineeInfo(int id)
        {
            return _traineeAppService.GetTraineeById(id);
        }

        /// <summary>Gets all trainees information in database</summary>
        /// <returns>All trainees information in database</returns>
        [Route("all")]
        [ActivityAuthorize(ActivityAuthorize.ExecuteDeltaSelectTrainee)]
        public ListSuitableTraineeDTO GetAllTrainee()
        {
            return _traineeAppService.GetAllTrainee();
        }

        /// <summary>Register a trainee in a training</summary>
        /// <param name="registerTraineeInput">The trainee input</param>
        /// <returns>Whether the trainee is registered in training or not</returns>
        [HttpPost,Route("register")]
        [ActivityAuthorize(ActivityAuthorize.ExecuteDeltaSelectTrainee)]
        public ResponseMessageDTO register(RegisterTraineeInputDTO registerTraineeInput)
        {
            return _traineeAppService.RegisterTrainee(registerTraineeInput);
        }

        /// <summary>Register Trainee to a Training Program based on Token/summary>
        /// <returns>Full trainee information based on Token</returns>
        [HttpPost, Route("register/profile")]
        [ActivityAuthorize(ActivityAuthorize.ExecuteDeltaExecute)]
        public ResponseMessageDTO profileRegister(RegisterTraineeProfileInputDTO registerTraineeProfileInput)
        {
            var headers = Request.Headers;
            string userAuthToken = headers.Authorization.ToString();
            return _traineeAppService.RegisterTraineeProfile(registerTraineeProfileInput, userAuthToken);
        }
    }
}