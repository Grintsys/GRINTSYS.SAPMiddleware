using Abp.BackgroundJobs;
using Abp.UI;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using GRINTSYS.SAPMiddleware.M2;
using GRINTSYS.SAPMiddleware.M2.Payments;
using GRINTSYS.SAPMiddleware.Mail;
using GRINTSYS.SAPMiddleware.Payments.Dto;
using GRINTSYS.SAPMiddleware.Payments.Job;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Payments
{
    public class PaymentAppService : SAPMiddlewareAppServiceBase, IPaymentAppService
    {
        private readonly UserManager _userManager;
        private readonly PaymentManager _paymentManager;
        private readonly IBackgroundJobManager _backgroundJobManager;

        public PaymentAppService(IBackgroundJobManager backgroundJobManager, 
            UserManager userManager,
            PaymentManager paymentManager)
        {
            _backgroundJobManager = backgroundJobManager;
            _userManager = userManager;
            _paymentManager = paymentManager;
        }

        public async Task AutorizePayment(GetPaymentInput input)
        {
            var userId = GetUserId();
            var user = await _userManager.FindByIdAsync(userId.ToString());

            await _backgroundJobManager.EnqueueAsync<PaymentJob, PaymentJobArgs>(
               new PaymentJobArgs
               {
                   Id = input.Id,
                   UserId = GetUserId(),
                   To = user.EmailAddress
            });
        }

        public async Task CreateInvoice(AddInvoiceInput input)
        {
            var invoice = ObjectMapper.Map<Invoice>(input);

            await _paymentManager.CreateInvoice(invoice);
        }

        public async Task CreatePayment(AddPaymentInput input)
        {
            var payment = ObjectMapper.Map<Payment>(input);

            payment.UserId = GetUserId();

            await _paymentManager.CreatePayment(payment);

            /*
            foreach (var item in input.PaymentItemList)
            {
            }
            */
        } 

        public async Task<PaymentOutput> DeclinePayment(GetPaymentInput input)
        {
            var entity = _paymentManager.GetPayment(input.Id);

            entity.Status = PaymentStatus.CanceladoPorFinanzas;

            var payment = _paymentManager.UpdatePayment(entity);

            /*
            _backgroundJobManager.Enqueue<EmailJob, EmailArgs>(new EmailArgs()
            {
                To = entity.User.EmailAddress,
                Subject = String.Format("Notificacion Pago Cancelado Por Finanzas, Id: {0}", entity.Id),
                Body = String.Format("Pago Id: {0}, Factura: {1}, Fue Cancelado Por Finanzas", entity.Id, entity.Invoice.DocumentCode),
            });*/

            var result = await new EmailHelper().Send(new EmailArgs()
            {
                To = entity.User.EmailAddress,
                Subject = String.Format("Notificacion Pago Cancelado Por Finanzas, Id: {0}", entity.Id),
                Body = String.Format("Pago Id: {0}, Fue Cancelado Por Finanzas", entity.Id),
            });

            return new PaymentOutput()
            {
                Id = payment.Id,
                DocEntry = payment.DocEntry,
                UserId = payment.UserId,
                GeneralAccount = payment.Bank.GeneralAccount,
                BankName = payment.Bank.Name,
                Status = ((PaymentStatus)payment.Status).ToString(),
                Type = ((PaymentType)payment.Type).ToString(),
                Comment = payment.Comment,
                PayedAmount = payment.PayedAmount,
                ReferenceNumber = payment.ReferenceNumber,
                LastErrorMessage = payment.LastMessage,
                CreationTime = payment.CreationTime,
                DebtCollectorId = payment.User.CollectId
            };
        }

        public async Task DeletePayment(DeletePaymentInput input)
        {
            var payment = _paymentManager.GetPayment(input.Id);

            var user = await GetCurrentUserAsync();

            if(payment.UserId != user.Id)
            {
                throw new UserFriendlyException("You can not delete a payment of another person");
            }

            await _paymentManager.DeletePayment(input.Id);
        }

        public PaymentOutput GetPayment(GetPaymentInput input)
        {
            var payment = _paymentManager.GetPayment(input.Id);

            return new PaymentOutput()
            {
                Id = payment.Id,
                DocEntry = payment.DocEntry,
                UserId = payment.UserId,
                GeneralAccount = payment.Bank.GeneralAccount,
                BankName = payment.Bank.Name,
                Status = ((PaymentStatus)payment.Status).ToString(),
                Type = ((PaymentType)payment.Type).ToString(),
                Comment = payment.Comment,
                PayedAmount = payment.PayedAmount,
                ReferenceNumber = payment.ReferenceNumber,
                LastErrorMessage = payment.LastMessage,
                CreationTime = payment.CreationTime,
                DebtCollectorId = payment.User.CollectId
            };
        }

        public List<Payment> GetPaymentsByUser(GetAllPaymentInput input)
        {
            var userId = GetUserId();

            return _paymentManager.GetPaymentsByUser(input.TenantId, 
                userId,
                DateTime.Parse(input.Begin), 
                DateTime.Parse(input.End));
        }
    }
}
