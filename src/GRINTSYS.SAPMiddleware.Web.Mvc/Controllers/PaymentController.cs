using Abp.AspNetCore.Mvc.Authorization;
using GRINTSYS.SAPMiddleware.Authorization;
using GRINTSYS.SAPMiddleware.Controllers;
using GRINTSYS.SAPMiddleware.Payments;
using GRINTSYS.SAPMiddleware.Payments.Dto;
using GRINTSYS.SAPMiddleware.Web.Models.Payments;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_M2Admin)]
    public class PaymentController : SAPMiddlewareControllerBase
    {
        private readonly IPaymentAppService _paymentService;

        public PaymentController(IPaymentAppService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<IActionResult> Index()
        {
            var payments = (_paymentService.GetPayments(new GetAllPaymentInput() { })).Items;

            var model = new PaymentListViewModel
            {
                Payments = payments
            };

            return View(model);
        }


        public async Task<ActionResult> Authorize(int paymentId)
        {
            await _paymentService.AutorizePayment(new GetPaymentInput() { Id = paymentId });

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Decline(int paymentId)
        {
            await _paymentService.DeclinePayment(new GetPaymentInput() { Id = paymentId });

            return RedirectToAction("Index");
        }
    }
}
