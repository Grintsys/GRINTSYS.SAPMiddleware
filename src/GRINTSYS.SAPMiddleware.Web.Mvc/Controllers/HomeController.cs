using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using GRINTSYS.SAPMiddleware.Controllers;
using GRINTSYS.SAPMiddleware.Orders;
using System;
using GRINTSYS.SAPMiddleware.Orders.Dto;
using System.Linq;
using GRINTSYS.SAPMiddleware.Payments;
using GRINTSYS.SAPMiddleware.Payments.Dto;

namespace GRINTSYS.SAPMiddleware.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : SAPMiddlewareControllerBase
    {
        private readonly IOrderAppService _orderService;
        private readonly IPaymentAppService _paymentService;

        public HomeController(IOrderAppService orderService, 
            IPaymentAppService paymentService)
        {
            _orderService = orderService;
            _paymentService = paymentService;
        }

        public ActionResult Index()
        {
            var currentDate = DateTime.Now;
            var begin = currentDate.AddDays(-currentDate.Day+1).ToShortDateString();
            var end = currentDate.AddDays(1).ToShortDateString();

            var orders = _orderService.GetOrders(new GetAllOrderInput() {
                begin = begin,
                end = end
            });

            var payments = _paymentService.GetPayments(new GetAllPaymentInput()
            {
                Begin = begin,
                End = end
            });

            ViewBag.SalesCountByThisMonth = orders.TotalCount;
            ViewBag.SalesAmountByThisMonth = orders.Items.Sum(s => s.Total);

            ViewBag.PaymentCountByThisMonth = payments.TotalCount;
            ViewBag.PaymentAmountByThisMonth = payments.Items.Sum(s => s.PayedAmount);

            return View();
        }
	}
}
