using Abp.Application.Services;

namespace RosenCDK
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class RosenCDKAppServiceBase : ApplicationService
    {
        protected RosenCDKAppServiceBase()
        {
            LocalizationSourceName = RosenCDKConsts.LocalizationSourceName;
        }
    }
}