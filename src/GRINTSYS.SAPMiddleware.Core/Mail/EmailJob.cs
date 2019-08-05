using Abp.BackgroundJobs;
using Abp.Dependency;
using System;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Mail
{
    public class EmailJob : BackgroundJob<EmailArgs>, ITransientDependency
    {
        public override void Execute(EmailArgs args)
        {
            try
            {
                var result = Task.Run(async () =>
                {
                    return await new EmailHelper().Send(args);
                });
            }catch(Exception e)
            {
                Logger.Error(e.Message);
            }
        }
    }
}
