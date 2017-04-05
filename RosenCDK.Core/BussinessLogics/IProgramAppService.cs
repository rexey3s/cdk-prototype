using Abp.Application.Services;
using RosenCDK.DTO;

namespace RosenCDK.BussinessLogics
{
    public interface IProgramAppService : IApplicationService
    {
        /// <summary>
        /// Get all Programs in database
        /// </summary>
        /// <returns>A ListProgramDTO object</returns>
        ListProgramDTO GetAllPrograms();

        /// <summary>
        /// Get Program by programID 
        /// </summary>
        /// <param name="programID">programID of program</param>
        /// <returns>A ProgramDetailDTO object</returns>
        ProgramDetailDTO GetProgramDetailByID(int programID);
    }
}
