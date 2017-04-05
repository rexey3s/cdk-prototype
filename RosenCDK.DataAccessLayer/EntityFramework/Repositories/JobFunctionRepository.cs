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
    public class JobFunctionRepository : RosenCDKRepositoryBase<JobFunction, int>, IJobFunctionRepository
    {
        public JobFunctionRepository(IDbContextProvider<RosenCDKDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public List<JobFunction> getJobFunctionsByJobFunctionIds(int[] jobfuntionID)
        {
            List<JobFunction> retList = new List<JobFunction>();
            foreach (int Id in jobfuntionID)
            {
                retList.Add(Get(Id));
            }
            return retList;
        }
    }
}
