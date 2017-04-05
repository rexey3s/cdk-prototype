using System.Reflection;
using Abp.Modules;

namespace RosenCDK
{
    public class RosenCDKCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
