using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Uow;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using GRINTSYS.SAPMiddleware.M2;
using GRINTSYS.SAPMiddleware.M2.Orders;
using GRINTSYS.SAPMiddleware.M2.Products;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Orders.Job
{
    public class OrderToSAPJob : BackgroundJob<int>, ITransientDependency
    {
        private readonly OrderManager _orderManager;
        private readonly UserManager _userManager;

        public OrderToSAPJob(OrderManager orderManager,
            UserManager userManager)
        {
            _orderManager = orderManager;
            _userManager = userManager;
        }

        public async Task SendToSap(int id)
        {
            Logger.Debug(String.Format("SendToSap({0})", id));
            string url = String.Format("{0}api/orders/{1}", ConfigurationManager.AppSettings["SAPEndpoint"], id);
            var response = await AppConsts.Instance.GetClient().GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                Logger.Info("Success to send to SAP");
            }
        }

        [UnitOfWork]
        public override async void Execute(int id)
        {
            try
            {
                await SendToSap(id);

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
            catch(Exception e)
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
