using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using GRINTSYS.SAPMiddleware.Authorization;
using GRINTSYS.SAPMiddleware.Controllers;
using GRINTSYS.SAPMiddleware.Roles;
using GRINTSYS.SAPMiddleware.Roles.Dto;
using GRINTSYS.SAPMiddleware.Web.Models.Roles;
using GRINTSYS.SAPMiddleware.SapInvoices;
using GRINTSYS.SAPMiddleware.PurchaseOrders;
using GRINTSYS.SAPMiddleware.SapInvoices.Dto;
using GRINTSYS.SAPMiddleware.Web.Models;
using GRINTSYS.SAPMiddleware.Web.Models.SapInvoices;
using GRINTSYS.SAPMiddleware.Orders.Dto;
using GRINTSYS.SAPMiddleware.PurchaseOrders.Dto;

namespace GRINTSYS.SAPMiddleware.Web.Mvc.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_M2Admin_SapInvoice)]
    public class SapInvoiceController : SAPMiddlewareControllerBase
    {
        private readonly ISapInvoiceAppService _sapInvoiceAppService;
        private readonly IPurchaseOrderAppService _purchaseOrderAppService;

        public SapInvoiceController(ISapInvoiceAppService sapInvoiceAppService,
            IPurchaseOrderAppService purchaseOrderAppService)
        {
            _sapInvoiceAppService = sapInvoiceAppService;
            _purchaseOrderAppService = purchaseOrderAppService;
        }

        public async Task<IActionResult> Index()
        {
            var invoices = _sapInvoiceAppService.GetAllSapInvoices().Items;

            var model = new SapInvoiceListViewModel
            {
                SapInvoices = invoices
            };

            return View(model);
        }

        public async Task<ActionResult> SendPurchaseOrderToSap(int invoiceId)
        {
            var purchaseOrder = _purchaseOrderAppService.GetPurchaseOrder(new GetPurchaseOrderInput() { Id = invoiceId });
        
            if (purchaseOrder.Id == 0)
            {
                await _purchaseOrderAppService.CreatePurchaseOrder(new GetPurchaseOrderInput() { Id = invoiceId });
            }
            
            await _purchaseOrderAppService.SendPurchaseOrderToSap(new GetPurchaseOrderInput() { Id = invoiceId });

            return RedirectToAction("Index");
        }
    }
}