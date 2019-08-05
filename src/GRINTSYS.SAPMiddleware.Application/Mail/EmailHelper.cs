using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Mail
{
    public class EmailHelper
    {
        public async Task<string> Send(EmailArgs args)
        {
            var apiKey = String.IsNullOrEmpty(args.ApiKey) ? ConfigurationManager.AppSettings["SendGripAPIKey"] : args.ApiKey;
            var fromEmail = String.IsNullOrEmpty(args.FromEmail) ? ConfigurationManager.AppSettings["FromEmail"] : args.FromEmail;
            var fromEmailDisplayName = String.IsNullOrEmpty(args.FromEmailDisplayName) ? ConfigurationManager.AppSettings["FromEmailDisplayName"] : args.FromEmailDisplayName;

            var mailClient = new SendGridClient(apiKey);
            var message = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail, fromEmailDisplayName),
                Subject = args.Subject,
                HtmlContent = args.Body
            };

            string[] toArray = args.To.Split(',');
            List<EmailAddress> tos = toArray
                .ToList()
                .Select(s => new EmailAddress(s))
                .ToList();

            message.AddTos(tos);

            if (!string.IsNullOrWhiteSpace(args.CC)) { message.AddBcc(new EmailAddress(args.CC)); }

            if (!string.IsNullOrWhiteSpace(args.BCC)) { message.AddBcc(new EmailAddress(args.BCC)); }

            var response = await mailClient.SendEmailAsync(message);
            return response.StatusCode.ToString();
        }
    }
}
