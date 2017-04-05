using Abp.Domain.Repositories;
using RosenCDK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosenCDK.Repositories
{
    public interface IJobFunctionRepository : IRepository<JobFunction, int>
    {   /**
         * IRepository.GetAllList() hoặc IRepository.GetAllListAsync() đã có sẵn -> tái sử dụng lại 
         **/
        //List<JobFunction> GetAllJobFunction();
        /**
        * Sử dụng IRepository.FirstOrDefault(int id) hoặc IRepository.First(int id), Lưu ý handle Exception của nó 
        **/
        //JobFunction GetJobFunctionByJobFunctionID(int jobFunctionID);
        List<JobFunction> getJobFunctionsByJobFunctionIds(int[] jobfuntionID);
    }
}
