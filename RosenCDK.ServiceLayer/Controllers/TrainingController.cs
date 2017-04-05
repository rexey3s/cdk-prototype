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
    /// TrainingController provides APIs to work with Trainings data
    /// </summary>
    [RoutePrefix("api/training")]
    public class TrainingController : AbpApiController
    {
        public ITrainingAppService _trainingAppService { get; set; }

        /// <summary>Gets all trainings information in database</summary>
        /// <returns>All trainings information in database</returns>
        [Route("all")]
        [ActivityAuthorize(ActivityAuthorize.TrainingManagementView)]
        public ListTrainingDTO GetAll()
        {
            return _trainingAppService.GetAllTrainings();
        }

        /// <summary>Cancels a training based on id</summary>
        /// <param name="id">The training id.</param>
        /// <returns>Whether the training is canceled or not</returns>
        [HttpGet, Route("cancel/{id:int}")]
        [ActivityAuthorize(ActivityAuthorize.TrainingManagementCancel)]
        public ResponseMessageDTO CancelTraining(int Id)
        {
            return _trainingAppService.CancelTraining(Id);
        }

        //Test lazy-load Training Status
        // Route: /api/training/1
        /// <summary>Gets a training information based on id</summary>
        /// <param name="id">The training id.</param>
        /// <returns>Full Training information</returns>
        [Route("info/{id:int}")]
        [ActivityAuthorize(ActivityAuthorize.TrainingManagementView)]
        public TrainingDTO GetTrainingInfo(int Id)
        {
            return _trainingAppService.GetTrainingDetail(Id);
        }

        /// <summary>Creates a training based on Input</summary>
        /// <param name="createTrainingInput">The training input.</param>
        /// <returns>Whether the training is created or not</returns>
        [HttpPost,Route("create")]
        [ActivityAuthorize(ActivityAuthorize.TrainingManagementNew)]
        public ResponseMessageDTO CreateTraining(CreateTrainingInputDTO createTrainingInput)
        {
            return _trainingAppService.CreateTraining(createTrainingInput);
        }

        /// <summary>Updates a training based on Input</summary>
        /// <param name="updateTrainingInput">The training input.</param>
        /// <returns>Whether the training is updated or not</returns>
        [HttpPost,Route("update")]
        [ActivityAuthorize(ActivityAuthorize.TrainingManagementUpdate)]
        public ResponseMessageDTO UpdateTraining(UpdateTrainingInputDTO updateTrainingInput)
        {
            return _trainingAppService.UpdateTraining(updateTrainingInput);
        }

        /// <summary>Completes a training based on Input</summary>
        /// <param name="completeTrainingInput">The training input.</param>
        /// <returns>Whether the training is updated or not</returns>
        [HttpPost,Route("complete")]
        [ActivityAuthorize(ActivityAuthorize.TrainingManagementComplete)]
        public ResponseMessageDTO CompleteTraining(CompleteTrainingInputDTO completeTrainingInput)
        {
            return _trainingAppService.CompleteTraining(completeTrainingInput);
        }

        /// <summary>Calculates a training based on Input</summary>
        /// <param name="calculateEndDateInput">The training end date input.</param>
        /// <returns>The end date</returns>
        [HttpPost, Route("calculateEndDate")]
        [ActivityAuthorize(ActivityAuthorize.TrainingManagementView)]
        public EndDateMessageDTO CalculateEndDate(CalculateEndDateInputDTO calculateEndDateInput)
        {
            return _trainingAppService.CalculaterEndDate(calculateEndDateInput);
        }

        /// <summary>Customizes a training end date on Input</summary>
        /// <param name="customEndDateInput">The training custom end date input.</param>
        /// <returns>The new end date</returns>
        [HttpPost,Route("customEndDate")]
        [ActivityAuthorize(ActivityAuthorize.TrainingManagementView)]
        public EndDateMessageDTO CustomEndDate(CustomEndDateInputDTO customEndDateInput)
        {
            return _trainingAppService.CustomEndDate(customEndDateInput);
        }
    }
}