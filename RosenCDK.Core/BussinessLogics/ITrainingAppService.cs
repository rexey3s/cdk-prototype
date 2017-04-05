using Abp.Application.Services;
using RosenCDK.DTO;

namespace RosenCDK.BussinessLogics
{
    public interface ITrainingAppService : IApplicationService
    {
        /// <summary>
        /// Get all Trainings in database
        /// </summary>
        /// <returns>A ListTrainingDTO object</returns>
        ListTrainingDTO GetAllTrainings();

        /// <summary>
        /// Get full Training's information and reference tables by trainingID 
        /// </summary>
        /// <param name="trainingID">trainingID of Training</param>
        /// <returns>A TrainingDetailDTO object</returns>
        TrainingDetailDTO GetTrainingDetailByID(int trainingID);

        /// <summary>
        /// Calculate the end date to train the Program 
        /// </summary>
        /// <param name="calculateEndDateInput">A CalculateEndDateInputDTO object included programId and start date</param>
        /// <returns>An EndDateMessageDTO object included end date</returns>
        EndDateMessageDTO CalculaterEndDate(CalculateEndDateInputDTO calculateEndDateInput);

        /// <summary>
        /// Calculate the end date to train the modules arrangement
        /// </summary>
        /// <param name="customEndDateInput">A CustomEndDateInputDTO object included modules arrangement and start date</param>
        /// <returns>An EndDateMessageDTO object included end date</returns>
        EndDateMessageDTO CustomEndDate(CustomEndDateInputDTO customEndDateInput);

        /// <summary>
        /// Insert a Training to database
        /// </summary>
        /// <param name="createTrainingInput">A CreateTrainingInputDTO object included information about Training</param>
        /// <returns>A ResponseMessageDTO object represent status (true/false) of creating Training process</returns>
        ResponseMessageDTO CreateTraining(CreateTrainingInputDTO createTrainingInput);

        /// <summary>
        /// Update Training's information in database
        /// </summary>
        /// <param name="updateTrainingInput">A UpdateTrainingInputDTO object included information about Training</param>
        /// <returns>A ResponseMessageDTO object represent status (true/false) of updating Training process</returns>
        ResponseMessageDTO UpdateTraining(UpdateTrainingInputDTO updateTrainingInput);

        /// <summary>
        /// Cancel a open Training by trainingID
        /// </summary>
        /// <param name="trainingID">The id of Training</param>
        /// <returns>A ResponseMessageDTO object represent status (true/false) of canceling Training process</returns>
        ResponseMessageDTO CancelTraining(int trainingID);

        /// <summary>
        /// Completed a open Training
        /// </summary>
        /// <param name="completeTrainingInput">A CompleteTrainingInputDTO object included trainingId and a JSON string which represent TraineeTrainingOutcomeDTO object</param>
        /// <returns>A ResponseMessageDTO object represent status (true/false) of completing Training process</returns>
        ResponseMessageDTO CompleteTraining(CompleteTrainingInputDTO completeTrainingInput);

        /// <summary>
        /// Get Training by trainingID 
        /// </summary>
        /// <param name="id">The id of Training</param>
        /// <returns>A TrainingDTO object</returns>
        TrainingDTO GetTrainingDetail(int id);

        /// <summary>
        /// Automatic maintain Trainings in a specific time of day
        /// </summary>
        void AutomaticMaintainTraining();
    }
}
