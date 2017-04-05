using Abp.Application.Services;
using RosenCDK.DTO;

namespace RosenCDK.BussinessLogics
{
    public interface IJobFunctionAppService : IApplicationService
    {
        /// <summary>
        /// Get a JobFunctionDTO object have jobFunctionId
        /// </summary>
        /// <param name="JobFunctionId">ID that you want to get JobFunction</param>
        /// <returns>JobFunctionDTO object</returns>
        JobFunctionDTO GetJobFunctionById(int jobFunctionId);

        /// <summary>
        /// Get all Job Fucntions in database
        /// </summary>
        /// <returns>A ListJobFunctionDTO object</returns>
        ListJobFunctionDTO GetAllJobFunctions();
    }
}
