using Abp.AspNetCore.Mvc.Authorization;
using GRINTSYS.SAPMiddleware.Authorization;
using GRINTSYS.SAPMiddleware.Controllers;
using GRINTSYS.SAPMiddleware.Payments;
using GRINTSYS.SAPMiddleware.Payments.Dto;
using GRINTSYS.SAPMiddleware.Web.Models.Payments;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

using Abp.Application.Services.Dto;
using GRINTSYS.SAPMiddleware.Web.Models.Users;
using GRINTSYS.SAPMiddleware.Users;
using GRINTSYS.SAPMiddleware.Banks;
using GRINTSYS.SAPMiddleware.Clients;

namespace GRINTSYS.SAPMiddleware.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_M2Admin)]
    public class PaymentsController : SAPMiddlewareControllerBase
    {
        private readonly IPaymentAppService _paymentService;
        private readonly IUserAppService _userAppService;
        private readonly IBankAppService _bankAppService;
        private readonly IClientAppService _clientAppService;
        
        public PaymentsController(IPaymentAppService paymentService, IUserAppService userService, IBankAppService bankAppService, IClientAppService clientAppService)
        {
            _paymentService = paymentService;
            _userAppService = userService;
            _bankAppService = bankAppService;
            _clientAppService = clientAppService;
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

        public async Task<ActionResult> EditPaymentModal(int paymentId)
        {
            var payment = _paymentService.GetPayment(new GetPaymentInput() { Id = paymentId });
            var client = _clientAppService.GetClientDocumentsByCardCode(new Clients.Dto.GetClientInput() { CardCode = payment.CardCode });
            payment.CardName = client.Name;

            var banks = await _bankAppService.GetAll(new Banks.Dto.GetAllBankInput() { TenantId = payment.TenantId });
            ViewBag.Banks = banks;
  
            var model = new EditPaymentModalViewModel
            {
                Payment = payment,
                PaymentItems = payment.PaymentItemsOutput,
                ClientInvoices = client.Invoices                
            };

            return View("_EditPaymentModal", model);
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
