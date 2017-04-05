using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RosenCDK.ServiceLayer.Authorizations
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple = false)]
    public class SkipFilterAttribute: Attribute
    {
    }
}