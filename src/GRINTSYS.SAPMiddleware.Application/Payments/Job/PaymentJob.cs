using Abp.BackgroundJobs;
using Abp.Dependency;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using GRINTSYS.SAPMiddleware.M2.Payments;
using GRINTSYS.SAPMiddleware.Mail;
using System;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Payments.Job
{
    public class PaymentJob : BackgroundJob<PaymentJobArgs>, ITransientDependency
    {
        public PaymentJob()
        {
        }

        public override void Execute(PaymentJobArgs args)
        {
            try
            {
                var result = Task.Run(async () =>
                {
                    return await new PaymentHttpClient().CreatePaymentOnSAP(args.Id);
                });
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);

                if (!String.IsNullOrEmpty(args.To))
                {
                    var result = new EmailHelper().Send(new EmailArgs() { Subject = "Notificacion de Error", Body = e.Message, To = args.To });
                }
            }
        }
    }
}
