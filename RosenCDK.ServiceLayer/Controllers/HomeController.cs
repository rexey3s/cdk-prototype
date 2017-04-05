using System.Web.Mvc;
using RosenCDK.ServiceLayer.Authorizations;

namespace RosenCDK.Web.Controllers
{
    public class HomeController : RosenCDKControllerBase
    {
       
        public ActionResult Index()
        { 
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}