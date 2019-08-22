using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using GRINTSYS.SAPMiddleware.Controllers;
using GRINTSYS.SAPMiddleware.Orders;
using System;
using GRINTSYS.SAPMiddleware.Orders.Dto;
using System.Linq;

namespace GRINTSYS.SAPMiddleware.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : SAPMiddlewareControllerBase
    {
        private readonly IOrderAppService _orderService;

        public HomeController(IOrderAppService orderService)
        {
            _orderService = orderService;
        }

        public ActionResult Index()
        {
            var currentDate = DateTime.Now;;

            var orders = _orderService.GetOrders(new GetAllOrderInput() {
                begin = currentDate.AddDays(-currentDate.Day).ToShortDateString(),
                end = currentDate.AddDays(1).ToShortDateString()
            });

            ViewBag.SalesCountByThisMonth = orders.TotalCount;
            ViewBag.SalesAmountByThisMonth = orders.Items.Sum(s => s.Total);

            return View();
        }
	}
}
