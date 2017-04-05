using Abp.Web.Mvc.Controllers;
using Abp.WebApi.Controllers;

namespace RosenCDK.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class RosenCDKControllerBase : AbpController
    {
        protected RosenCDKControllerBase()
        {
            LocalizationSourceName = RosenCDKConsts.LocalizationSourceName;
        }
    }
}