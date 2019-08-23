using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Net.Mail;
using Castle.Core.Logging;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using GRINTSYS.SAPMiddleware.M2;
using GRINTSYS.SAPMiddleware.M2.Orders;
using GRINTSYS.SAPMiddleware.M2.Products;
using GRINTSYS.SAPMiddleware.Mail;
using GRINTSYS.SAPMiddleware.Orders.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Orders.Job
{
    public class OrderJob : BackgroundJob<OrderParams>, ITransientDependency
    {
        private readonly ProductManager _productManager;
        private readonly OrderManager _orderManager;
        private readonly CartManager _cartManager;

        public OrderJob(ProductManager productManager, 
            OrderManager orderManager, 
            CartManager cartManager)
        {
            _productManager = productManager;
            _orderManager = orderManager;
            _cartManager = cartManager;
        }

        public override async void Execute(OrderParams args)
        {
            try
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

                var orderId = await _orderManager.CreateOrder(newOrder);

                var products = _cartManager.GetCartProductItemsByUser(args.UserId, args.TenantId);

                foreach (var item in products)
                {
                    var newOrderItem = new OrderItem()
                    {
                        TenantId = args.TenantId,
                        OrderId = orderId,
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

                //Delete the user cart
                await _cartManager.DeleteUserCart(args.UserId, args.TenantId);

                //Hey this send to SAP
                string url = String.Format("{0}api/orders/{1}", ConfigurationManager.AppSettings["SAPEndpoint"], orderId);
                //var response = await AppConsts.Instance.GetClient().GetAsync(url);

                using (var Client = new HttpClient())
                {
                    Client.Timeout = TimeSpan.FromMinutes(5);
                    Client.DefaultRequestHeaders.Accept.Clear();
                    Client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await Client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        Logger.Info("Success to send to SAP");
                    }
                }


                /*
                //Finally send a mail
                var user = await _userManager.GetUserByIdAsync(args.UserId);

                if (user == null)
                    return;

                await new EmailHelper().Send(new EmailArgs
                {
                    Subject = String.Format("Confirmación de pedido para cliente {0} ha sido Creado En M2", args.CardCode),
                    Body = "",
                    To = user.EmailAddress
                });*/

            }
            catch (Exception e)
            {
                Logger.Error(e.Message);

                /*
                var user = await _userManager.GetUserByIdAsync(args.UserId);

                if (user == null)
                    return;

                await new EmailHelper().Send(new EmailArgs {
                    Subject = String.Format("Error Pedido Cliente {0}", args.CardCode),
                    Body = e.Message,
                    To = user.EmailAddress
                });*/
            }
        }
    }
}
