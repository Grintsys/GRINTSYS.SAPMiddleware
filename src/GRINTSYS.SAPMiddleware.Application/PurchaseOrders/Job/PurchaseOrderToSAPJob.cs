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


namespace GRINTSYS.SAPMiddleware.PurchaseOrders.Job
{
    public class PurchaseOrderToSAPJob : BackgroundJob<int>, ITransientDependency
    {
        public PurchaseOrderToSAPJob()
        {

        }

        public async Task SendToSap(int id)
        {
            Logger.Debug(String.Format("SendToSap({0})", id));
            string url = String.Format("{0}api/purchaseorders/{1}", ConfigurationManager.AppSettings["SAPEndpoint"], id);
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
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }
    }
}
