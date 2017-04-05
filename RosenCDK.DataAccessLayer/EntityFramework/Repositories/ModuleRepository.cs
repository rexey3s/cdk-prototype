using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using RosenCDK.Entities;
using RosenCDK.Repositories;

namespace RosenCDK.EntityFramework.Repositories
{
    public class ModuleRepository : RosenCDKRepositoryBase<Module, int>, IModuleRepository
    {
        public ModuleRepository(IDbContextProvider<RosenCDKDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public List<Module> GetModulesByArrayID(int[] moduleIDs)
        {
            List<Module> retList = new List<Module>();
            foreach (int Id in moduleIDs)
            {
                retList.Add(Get(Id));
            }
            return retList;
        }
    }
}
