using Abp.Application.Services;
using RosenCDK.DTO;

namespace RosenCDK.BussinessLogics
{
    public interface ITraineeAppService : IApplicationService
    {
        /// <summary>
        /// Get trainee info with traineeId
        /// </summary>
        /// <param name="traineeId">ID of trainee</param>
        /// <returns>A TraineeDTO object</returns>
        TraineeDTO GetTraineeById(int traineeId);

        /// <summary>
        /// Get trainee info and detail job functions, competencies, programs targeted, training attented of this trainee by authorized token
        /// </summary>
        /// <param name="personAuthToken">Authorize token of Trainee</param>
        /// <returns>A FullTraineeDetailDTO object</returns>
        FullTraineeDetailDTO GetFullTraineeInfo(string personAuthToken);

        /// <summary>
        /// Register a trainee to a open training
        /// </summary>
        /// <param name="registerTraineeInput">Register inputs included traineeId, programId and trainingId</param>
        /// <returns>A ResponseMessageDTO object that show the status of register process</returns>
        ResponseMessageDTO RegisterTrainee(RegisterTraineeInputDTO registerTraineeInput);

        /// <summary>
        /// Register a trainee to a open training using his/her authorized token
        /// </summary>
        /// <param name="registerTraineeInput">Register inputs included programId and trainingId</param>
        /// <returns>A ResponseMessageDTO object that show the status of register process</returns>
        ResponseMessageDTO RegisterTraineeProfile(RegisterTraineeProfileInputDTO registerTraineeProfileInput, string personAuthToken);
        /// <summary>
        /// Get list suitable Trainees to assigned to Program by programID 
        /// </summary>
        /// <param name="programID">programID of Program</param>
        /// <returns>A ListSuitableTraineeDTO object</returns>
        ListSuitableTraineeDTO GetSuitableTrainees(int programID);

        /// <summary>
        /// Get all Trainees in database
        /// </summary>
        /// <returns>A ListSuitableTraineeDTO object</returns>
        ListSuitableTraineeDTO GetAllTrainee();
    }
}
