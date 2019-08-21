using Abp.BackgroundJobs;
using Abp.Dependency;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using GRINTSYS.SAPMiddleware.M2.Payments;
using GRINTSYS.SAPMiddleware.Mail;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Payments.Job
{
    public class PaymentJob : BackgroundJob<PaymentJobArgs>, ITransientDependency
    {
        public PaymentJob()
        {
        }

        public async Task GetPayment(int id)
        {
            Logger.Debug(String.Format("SendToSap({0})", id));
            string url = String.Format("{0}api/payments/{1}", ConfigurationManager.AppSettings["SAPEndpoint"], id);
            var response = await AppConsts.Instance.GetClient().GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                Logger.Info("Success to send to SAP");
            }
        }

        public override void Execute(PaymentJobArgs args)
        {
             GetPayment(args.Id);
        }
    }
}
