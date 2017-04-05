using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using RosenCDK.Entities;

namespace RosenCDK.Repositories
{
    public interface IRoleRepository : IRepository<Role, int>
    {
        List<Activity> GetActivityByRoleId(int roleId);
    }
}
