using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Abp.WebApi.Controllers;
using RosenCDK.BussinessLogics;
using RosenCDK.DTO;
using ServiceLayer.Authorization;

namespace RosenCDK.ServiceLayer.Controllers
{
    /// <summary>
    /// DataController provides APIs to Execute Data Analysis
    /// </summary>
    [RoutePrefix("api/analysis")]
    public class DataController : AbpApiController
    {
        public IExecutedDeltaAnalysisAppService _executedDeltaAnalysisAppService { get; set; }
        public ITraineeAppService _traineeAppService { get; set; }

        /// <summary>Executes Data Analysis based on Input</summary>
        /// <param name="executeDataInput">Executes Data Analysis input</param>
        /// <returns>The result of Executes Data Analysis based on input</returns>
        [HttpPost,Route("execute")]
        [ActivityAuthorize(ActivityAuthorize.ExecuteDeltaSelectTrainee)]
        public ListExecuteDataDTO ExecuteDeltaAnalysis(ExecuteDataInputDTO executeDataInput)
        {
            return _executedDeltaAnalysisAppService.ExecuteDeltaAnalysis(executeDataInput.JobFunctionId,
                executeDataInput.TraineeId);
        }

        /// <summary>Executes Data Analysis based on Profile</summary>
        /// <param name="executeDataInput">Executes Data Analysis input based on profile</param>
        /// <returns>The result of Executes Data Analysis based on profile</returns>
        [HttpPost,Route("execute/profile")]
        [ActivityAuthorize(ActivityAuthorize.ExecuteDeltaExecute)]
        public ListExecuteDataDTO ExcuteDataAnalysisProfile(ExecuteDataInputDTO executeDataInput)
        {
            var headers = Request.Headers;
            string userAuthToken = headers.Authorization.ToString();
            int traineeId = _traineeAppService.GetFullTraineeInfo(userAuthToken).TraineeID;
            return _executedDeltaAnalysisAppService.ExecuteDeltaAnalysis(executeDataInput.JobFunctionId, traineeId);
        }
    }
}