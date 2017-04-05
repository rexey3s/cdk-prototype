using System.Reflection;
using System.Web.Http;
using System.Web.Http.Filters;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Modules;
using Abp.WebApi;
using RosenCDK.Authorizations;
using RosenCDK.BussinessLogics;
using ServiceLayer.Authorization;

namespace RosenCDK
{
    [DependsOn(typeof(AbpWebApiModule), typeof(RosenCDKApplicationModule))]
    public class RosenCDKWebApiModule : AbpModule
    {
        
        public override void PreInitialize()
        {
            base.PreInitialize();
            Configuration.Modules.AbpWebApi().HttpConfiguration.MapHttpAttributeRoutes();
            Configuration.Modules.AbpWebCommon().SendAllExceptionsToClients = true;

        }

        public override void Initialize()
        { 
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            IocManager.Register<IAuthorizationFilter, ActivityAuthorizationFilter>(DependencyLifeStyle.Transient);
            var authorizationFilter = IocManager.Resolve<IAuthorizationFilter>();
            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(authorizationFilter);
        }
    }
}
