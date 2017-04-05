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
    public class TrainingStatusRepository : RosenCDKRepositoryBase<TrainingStatus,int>, ITrainingStatusRepository
    {
        public TrainingStatusRepository(IDbContextProvider<RosenCDKDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public int GetTrainingStatusIdByName(string statusName)
        {
            return Context.TrainingStatuses.Where(status => status.StatusName == statusName).FirstOrDefault().Id;
        }
    }
}
