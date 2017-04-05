using Abp.Application.Services;
using RosenCDK.DTO;

namespace RosenCDK.BussinessLogics
{
    public interface ITrainerAppService : IApplicationService
    {
        /// <summary>
        /// Get list suitable Trainers to assigned to program by programID 
        /// </summary>
        /// <param name="programID">programID of program</param>
        /// <returns>A ListSuitableTrainerDTO object</returns>
        ListSuitableTrainerDTO GetSuitableTrainers(int programID);
    }
}
