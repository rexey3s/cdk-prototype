using Abp.Web.Mvc.Views;

namespace RosenCDK.Web.Views
{
    public abstract class RosenCDKWebViewPageBase : RosenCDKWebViewPageBase<dynamic>
    {

    }

    public abstract class RosenCDKWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected RosenCDKWebViewPageBase()
        {
            LocalizationSourceName = RosenCDKConsts.LocalizationSourceName;
        }
    }
}