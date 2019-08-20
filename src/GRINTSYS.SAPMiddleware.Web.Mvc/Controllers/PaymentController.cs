using Abp.AspNetCore.Mvc.Authorization;
using GRINTSYS.SAPMiddleware.Authorization;
using GRINTSYS.SAPMiddleware.Controllers;
using GRINTSYS.SAPMiddleware.Orders;
using GRINTSYS.SAPMiddleware.Orders.Dto;
using GRINTSYS.SAPMiddleware.Payments;
using GRINTSYS.SAPMiddleware.Payments.Dto;
using GRINTSYS.SAPMiddleware.Web.Models.Orders;
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


        public async Task<ActionResult> SendToSap(int id)
        {
            int a = 1;

            //var output = await _roleAppService.GetRoleForEdit(new EntityDto(roleId));
            //var model = new EditRoleModalViewModel(output);

            //return View("_EditRoleModal", model);

            return RedirectToAction("Index");
        }
    }
}
