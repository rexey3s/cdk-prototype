using Abp.Domain.Repositories;
using RosenCDK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosenCDK.Repositories
{
    public interface IProgramRepository : IRepository<Program, int>
    {
        /**
         * IRepository.GetAllList() hoặc IRepository.GetAllListAsync() đã có sẵn -> tái sử dụng lại 
         **/
        //List<Program> GetAllProgram();
        List<Program> GetProgramsByIds(int[] programID);

    }
}
