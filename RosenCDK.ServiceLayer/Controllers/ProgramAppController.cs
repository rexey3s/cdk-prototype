using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Abp.WebApi.Controllers;
using RosenCDK.BussinessLogics;
using RosenCDK.DTO;
using RosenCDK.ServiceLayer.Authorizations;
using ServiceLayer.Authorization;

namespace RosenCDK.ServiceLayer.Controllers
{
    /// <summary>
    /// DataController provides APIs to work with Programs data
    /// </summary>
    [RoutePrefix("api/program")]
    public class ProgramAppController : AbpApiController
    {
        public IProgramAppService _programAppService { get; set; }
        public ITraineeAppService _traineeAppService { get; set; }
        public ITrainerAppService _trainerAppService { get; set; }

        /// <summary>Gets all trainings information</summary>
        /// <returns>Full Trainings Information</returns>
        [Route("all")]
        [SkipFilter]
        [ActivityAuthorize(ActivityAuthorize.ProgramManagementView)]
        public ListProgramDTO GetAll()
        {
            return _programAppService.GetAllPrograms();
        }

        /// <summary>Gets Training information based on id</summary>
        /// <param name="id">The training id</param>
        /// <returns>The result training information based on id</returns>
        [Route("info/{id:int}")]
        [ActivityAuthorize(ActivityAuthorize.ProgramManagementView)]
        public ProgramDetailDTO GetProgramInfo(int id)
        {
            return _programAppService.GetProgramDetailByID(id);
        }

        /// <summary>Gets Suitable Trainees for a training</summary>
        /// <param name="id">The training id</param>
        /// <returns>Suitable Trainees for that training</returns>
        [Route("getSuitableTrainees/{id:int}")]
        [ActivityAuthorize(ActivityAuthorize.ProgramManagementView)]
        public ListSuitableTraineeDTO GetSuitableTrainees(int id)
        {
            return _traineeAppService.GetSuitableTrainees(id);
        }

        /// <summary>Gets Suitable Trainers for a training</summary>
        /// <param name="id">The training id</param>
        /// <returns>Suitable Trainers for that training</returns>
        [Route("getSuitableTrainers/{id:int}")]
        [ActivityAuthorize(ActivityAuthorize.ProgramManagementView)]
        public ListSuitableTrainerDTO GetSuitableTrainers(int id)
        {
            return _trainerAppService.GetSuitableTrainers(id);
        }
    }
}