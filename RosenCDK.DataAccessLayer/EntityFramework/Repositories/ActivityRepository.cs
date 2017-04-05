using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.EntityFramework;
using RosenCDK.Entities;
using RosenCDK.Repositories;

namespace RosenCDK.EntityFramework.Repositories
{
    public class ActivityRepository : RosenCDKRepositoryBase<Activity, int>, IActivityRepository
    {
        public ActivityRepository(IDbContextProvider<RosenCDKDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public List<Activity> GetActivitysByRoleId(int roleId)
        {
            throw new NotImplementedException();
        }
    }
}
