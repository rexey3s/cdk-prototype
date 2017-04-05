using System.Reflection;
using Abp.Modules;
using RosenCDK.BussinessLogics;

namespace RosenCDK
{
    [DependsOn(typeof(RosenCDKCoreModule))]
    public class RosenCDKApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            ITrainingAppService trainingAppService = IocManager.Resolve<ITrainingAppService>();
            // Call once at application startup check setup a simple scheduler 
            // to update training status of training at pre-defined time
            trainingAppService.AutomaticMaintainTraining();
        }
    }
}
