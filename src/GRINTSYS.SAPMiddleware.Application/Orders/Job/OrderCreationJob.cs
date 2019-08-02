using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Net.Mail;
using GRINTSYS.SAPMiddleware.M2;
using GRINTSYS.SAPMiddleware.M2.Orders;
using GRINTSYS.SAPMiddleware.M2.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Orders.Job
{
    public class OrderCreationJob : BackgroundJob<CreateOrderParams>, ITransientDependency
    {
        private ProductManager _productManager;
        private OrderManager _orderManager;
        private CartManager _cartManager;
        private readonly IEmailSender _emailSender;

        public OrderCreationJob(ProductManager productManager, 
            OrderManager orderManager, 
            CartManager cartManager, 
            IEmailSender emailSender)
        {
            _productManager = productManager;
            _orderManager = orderManager;
            _cartManager = cartManager;
            _emailSender = emailSender;
        }

        public async Task CreateOrder(CreateOrderParams args)
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
        public override async void Execute(CreateOrderParams args)
        {
            try
            {
                await CreateOrder(args);

            }catch(Exception e)
            {
                Logger.Error(e.Message);
            }
        }
    }
}
