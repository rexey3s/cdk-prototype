using Abp.Domain.Repositories;
using RosenCDK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosenCDK.Repositories
{
    public interface IConfigurationRepository : IRepository<Configuration, int>
    {
        int GetMaximumHoursPerDay();
        string GetDaysOff();
        int GetMaintenanceTime();
    }
}
