using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using GRINTSYS.SAPMiddleware.Authorization;
using GRINTSYS.SAPMiddleware.Controllers;
using GRINTSYS.SAPMiddleware.Roles;
using GRINTSYS.SAPMiddleware.Roles.Dto;
using GRINTSYS.SAPMiddleware.Web.Models.Roles;
using GRINTSYS.SAPMiddleware.Orders;
using GRINTSYS.SAPMiddleware.Orders.Dto;
using GRINTSYS.SAPMiddleware.Web.Models;
using GRINTSYS.SAPMiddleware.Web.Models.Orders;

namespace GRINTSYS.SAPMiddleware.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_M2Admin)]
    public class OrderController : SAPMiddlewareControllerBase
    {
        private readonly IOrderAppService _orderService;

        public OrderController(IOrderAppService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var orders = (_orderService.GetOrders(new GetAllOrderInput() { })).Items;

            var model = new OrderListViewModel
            {
                Orders = orders
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
