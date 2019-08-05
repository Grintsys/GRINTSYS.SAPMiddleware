using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Net.Mail;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using GRINTSYS.SAPMiddleware.M2.Payments;
using GRINTSYS.SAPMiddleware.Mail;
using GRINTSYS.SAPMiddleware.SAP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Payments.Job
{
    public class PaymentJob : BackgroundJob<PaymentJobArgs>, ITransientDependency
    {
        private readonly SapDocument _sapDocument;
        private readonly UserManager _userManager;

        public PaymentJob(PaymentManager paymentManager, UserManager userManager)
        {
            _sapDocument = new SapPayment(paymentManager);
            _userManager = userManager;
        }

        public override void Execute(PaymentJobArgs args)
        {
            try
            {
                _sapDocument.Execute(new SapDocumentInput() { Id = args.Id });
            }catch(Exception e)
            {
                Logger.Error(e.Message);

                var user = _userManager.FindByIdAsync(args.UserId.ToString());

                if (user == null)
                    return;

                var email = new EmailHelper().Send(new EmailArgs
                {
                    Subject = String.Format("Error Al crear Pago con Id {0}", args.Id),
                    Body = e.Message,
                    To = user.Result.EmailAddress
                });
            }
        }
    }
}
