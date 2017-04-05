using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Castle.Components.DictionaryAdapter;
using RosenCDK.Entities;
using RosenCDK.Repositories;

namespace RosenCDK.EntityFramework.Repositories
{
    public class RoleRepository : RosenCDKRepositoryBase<Role, int>, IRoleRepository
    {
        public RoleRepository(IDbContextProvider<RosenCDKDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public List<Activity> GetActivityByRoleId(int roleId)
        {
            List<Activity> activitiesByRoleId = new EditableList<Activity>();
            var roleDistributionQuery = Context.RoleDistributions.Where(rd => rd.RoleId == roleId);
            roleDistributionQuery.ToList().ForEach(rd =>
            {
                activitiesByRoleId.Add(rd.Activity);
            });
            return activitiesByRoleId;
            
        }
    }
}
