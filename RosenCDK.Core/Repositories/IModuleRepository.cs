using Abp.Domain.Repositories;
using RosenCDK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosenCDK.Repositories
{
    public interface IModuleRepository : IRepository<Module, int>
    {
        /**
         * IRepository.GetAllList() hoặc IRepository.GetAllListAsync() đã có sẵn -> tái sử dụng lại 
         **/
        //List<Module> GetAllModule();
        /**
         * Sử dụng IRepository.FirstOrDefault(int id) hoặc IRepository.First(int id), Lưu ý handle Exception của nó 
         **/
        //Module GetModuleByID(int moduleID);
        List<Module> GetModulesByArrayID(int[] moduleIDs);
    }
}
