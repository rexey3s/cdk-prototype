using Abp.Application.Services;
using RosenCDK.DTO;

namespace RosenCDK.BussinessLogics
{
    public interface IExecutedDeltaAnalysisAppService : IApplicationService
    {
        /// <summary>
        /// Get ListExecuteDataDTO object that represent the data of Execute Delta Analysis process. We'll return it to Service Layer
        /// </summary>
        /// <param name="jobFunctionId">The id of job function</param>
        /// <param name="traineeId">The id of trainee</param>
        /// <returns>ListExecuteDataDTO object that represent the data of Execute Delta Analysis process</returns>
        ListExecuteDataDTO ExecuteDeltaAnalysis(int jobFunctionId, int traineeId);
    }
}
