using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using RosenCDK.EntityFramework;

namespace RosenCDK
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(RosenCDKCoreModule))]
    public class RosenCDKDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<RosenCDKDbContext>(null);
        }
    }
}
