using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using GRINTSYS.SAPMiddleware.Controllers;
using GRINTSYS.SAPMiddleware.Orders;
using System;
using GRINTSYS.SAPMiddleware.Orders.Dto;
using System.Linq;
using GRINTSYS.SAPMiddleware.Payments;
using GRINTSYS.SAPMiddleware.Payments.Dto;
using GRINTSYS.SAPMiddleware.Web.Models.Orders;
using Abp.Runtime.Session;

namespace GRINTSYS.SAPMiddleware.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : SAPMiddlewareControllerBase
    {
        private readonly IOrderAppService _orderService;
        private readonly IPaymentAppService _paymentService;
        private readonly IAbpSession _session;
        public HomeController(IOrderAppService orderService, 
            IPaymentAppService paymentService,
            IAbpSession session)
        {
            _orderService = orderService;
            _paymentService = paymentService;
            _session = session;
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

            var sellers = orders.Items.GroupBy(g => g.UserText).Select(s => new SellerListViewModel { SellerName = s.Key, Total = s.Sum(a => a.Total) }).ToList();
            //var salesbydaysofthemonth = orders.Items.GroupBy(g => g.CreationTime.Day).Select(s => new SalesByTheMonthDayListViewModel { Day = s.Key, Total = s.Sum(a => a.Total) }).ToList();

            ViewBag.Sellers = sellers;
            //ViewBag.SalesByTheMonthDays = String.Join(',', salesbydaysofthemonth);
            ViewBag.SalesCountByThisMonth = orders.TotalCount;
            ViewBag.SalesAmountByThisDay = 0; //orders.Items.Where(w => w.CreationTime.Day == DateTime.Now.Day).Sum(s => s.Total);
            ViewBag.SalesAmountByThisWeek = 0; //orders.Items.Where(w => w.CreationTime.da Sum(s => s.Total);
            ViewBag.SalesAmountByThisMonth = sellers.Sum(s => s.Total);

            ViewBag.PaymentCountByThisMonth = payments.TotalCount;
            ViewBag.PaymentAmountByThisMonth = payments.Items.Sum(s => s.PayedAmount);

            return View();
        }
	}
}
