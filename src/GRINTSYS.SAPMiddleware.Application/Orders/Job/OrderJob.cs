using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Net.Mail;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using GRINTSYS.SAPMiddleware.M2;
using GRINTSYS.SAPMiddleware.M2.Orders;
using GRINTSYS.SAPMiddleware.M2.Products;
using GRINTSYS.SAPMiddleware.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Orders.Job
{
    public class OrderJob : BackgroundJob<OrderParams>, ITransientDependency
    {
        private readonly ProductManager _productManager;
        private readonly OrderManager _orderManager;
        private readonly CartManager _cartManager;
        private readonly UserManager _userManager;

        public OrderJob(ProductManager productManager, 
            OrderManager orderManager, 
            CartManager cartManager,
            UserManager userManager)
        {
            _productManager = productManager;
            _orderManager = orderManager;
            _cartManager = cartManager;
            _userManager = userManager;
        }

        public async Task CreateOrder(OrderParams args)
        {
            var newOrder = new Order()
            {
                TenantId = args.TenantId,
                UserId = args.UserId,
                Status = OrderStatus.CreadoEnAplicacion,
                DeliveryDate = args.DeliveryDate,
                Comment = args.Comment,
                CardCode = args.CardCode
            };

            var order = await _orderManager.CreateOrder(newOrder);

            var products = _cartManager.GetCartProductItemsByUser(args.UserId, args.TenantId);

            foreach (var item in products)
            {
                var newOrderItem = new OrderItem()
                {
                    TenantId = args.TenantId,
                    OrderId = order.Id,
                    Code = item.Variant.Code,
                    Quantity = item.Quantity,
                    Price = item.Variant.Price,
                    TaxCode = "", //falta esto
                    WarehouseCode = item.Variant.WareHouseCode
                };

                await _orderManager.CreateOrderItem(newOrderItem);
                //actualiza el comprometido pero solo en M2
                await _productManager.UpdateProductStock(item.Variant.Id, item.Quantity);
            }

            //delete the user cart
            await _cartManager.DeleteUserCart(args.UserId, args.TenantId);
        }

        [UnitOfWork]
        public override async void Execute(OrderParams args)
        {
            try
            {
                await CreateOrder(args);

            }catch(Exception e)
            {
                Logger.Error(e.Message);

                var user = await _userManager.FindByIdAsync(args.UserId.ToString());

                if (user == null)
                    return;

                await new EmailHelper().Send(new EmailArgs {
                    Subject = String.Format("Error Pedido Cliente {0}", args.CardCode),
                    Body = e.Message,
                    To = user.EmailAddress
                });
            }
        }
    }
}
