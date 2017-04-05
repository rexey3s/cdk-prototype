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
    public class RoleDistributionRepository : RosenCDKRepositoryBase<RoleDistribution, int>, IRoleDistributionRepository
    {
        public RoleDistributionRepository(IDbContextProvider<RosenCDKDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
