using Microsoft.AspNetCore.Antiforgery;
using GRINTSYS.SAPMiddleware.Controllers;

namespace GRINTSYS.SAPMiddleware.Web.Host.Controllers
{
    public class AntiForgeryController : SAPMiddlewareControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
