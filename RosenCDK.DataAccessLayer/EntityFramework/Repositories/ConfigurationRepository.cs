using RosenCDK.Entities;
using RosenCDK.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using RosenCDK.EntityFramework.Repositories;

namespace RosenCDK.EntityFramework.Repositories
{
    public class ConfigurationRepository : RosenCDKRepositoryBase<Configuration, int>, IConfigurationRepository
    {
        public ConfigurationRepository(IDbContextProvider<RosenCDKDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public int GetMaximumHoursPerDay()
        {
            Configuration config = FirstOrDefault(name => name.Name == "MaximumHoursPerDay");
            return int.Parse(config.Value);
        }
        public string GetDaysOff()
        {
            Configuration config = FirstOrDefault(configuration => configuration.Name == "DaysOff");
            return config.Value;
        }

        public int GetMaintenanceTime()
        {
            Configuration config = FirstOrDefault(configuration => configuration.Name == "MaintenanceTime");
            return int.Parse(config.Value);
        }
    }
}
