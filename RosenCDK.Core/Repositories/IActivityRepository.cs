using Abp.Domain.Repositories;
using RosenCDK.Entities;
using System.Collections.Generic;

namespace RosenCDK.Repositories
{
    public interface IActivityRepository : IRepository<Activity, int>
    {
        /**
         * Nếu cần phải truy xuất tới TableRole thì cần được xử lý trên tầng BusinessLogic 
         **/
        List<Activity> GetActivitysByRoleId(int roleId);
    }
}
