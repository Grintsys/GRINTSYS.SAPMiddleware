using Abp.BackgroundJobs;
using GRINTSYS.SAPMiddleware.M2;
using GRINTSYS.SAPMiddleware.M2.Payments;
using GRINTSYS.SAPMiddleware.Payments.Dto;
using GRINTSYS.SAPMiddleware.Payments.Job;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Payments
{
    public class PaymentAppService : SAPMiddlewareAppServiceBase, IPaymentAppService
    {
        private readonly PaymentManager _paymentManager;
        private readonly IBackgroundJobManager _backgroundJobManager;

        public PaymentAppService(IBackgroundJobManager backgroundJobManager, PaymentManager paymentManager)
        {
            _backgroundJobManager = backgroundJobManager;
            _paymentManager = paymentManager;
        }

        public Task AutorizePayment(GetPaymentInput input)
        {
            return _backgroundJobManager.EnqueueAsync<PaymentJob, PaymentJobArgs>(
               new PaymentJobArgs
               {
                   Id = input.Id,
                   UserId = GetUserId()
               });
        }

        public Task CreateInvoice(AddInvoiceInput input)
        {
            var invoice = ObjectMapper.Map<Invoice>(input);

            return _paymentManager.CreateInvoice(invoice);
        }

        public Task CreatePayment(AddPaymentInput input)
        {
            var payment = ObjectMapper.Map<Payment>(input);

            payment.UserId = GetUserId();

            return _paymentManager.CreatePayment(payment);
        } 

        public PaymentOutput DeclinePayment(GetPaymentInput input)
        {
            var entity = _paymentManager.GetPayment(input.Id);

            entity.Status = PaymentStatus.CanceladoPorFinanzas;

            var payment = _paymentManager.UpdatePayment(entity);

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
                InvoiceNumber = payment.Invoice.DocumentCode,
                ReferenceNumber = payment.ReferenceNumber,
                LastErrorMessage = payment.LastMessage,
                CreationTime = payment.CreationTime,
                DebtCollectorId = payment.User.CollectId
            };
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
                InvoiceNumber = payment.Invoice.DocumentCode,
                ReferenceNumber = payment.ReferenceNumber,
                LastErrorMessage = payment.LastMessage,
                CreationTime = payment.CreationTime,
                DebtCollectorId = payment.User.CollectId
            };
        }

        public List<Payment> GetPaymentsByUser(GetAllPaymentInput input)
        {
            var userId = GetUserId();

            return _paymentManager.GetPaymentsByUser(input.TenantId, userId, input.Begin, input.End);
        }
    }
}
