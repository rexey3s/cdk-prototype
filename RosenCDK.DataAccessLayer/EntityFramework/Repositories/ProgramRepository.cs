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
    public class ProgramRepository : RosenCDKRepositoryBase<Program, int> ,IProgramRepository
    {
        public ProgramRepository(IDbContextProvider<RosenCDKDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public List<Program> GetProgramsByIds(int[] programID)
        {
            List<Program> retList = new List<Program>();
            foreach (int id in programID)
            {
                retList.Add(Get(id));
            }
            return retList;
        }
    }
}
