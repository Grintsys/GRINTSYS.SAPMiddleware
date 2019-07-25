using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using GRINTSYS.SAPMiddleware.Controllers;

namespace GRINTSYS.SAPMiddleware.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : SAPMiddlewareControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
