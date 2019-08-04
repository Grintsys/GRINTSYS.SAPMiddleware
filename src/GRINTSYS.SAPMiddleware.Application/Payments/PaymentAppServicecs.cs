using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using GRINTSYS.SAPMiddleware.M2;
using GRINTSYS.SAPMiddleware.M2.Payments;
using GRINTSYS.SAPMiddleware.Payments.Dto;

namespace GRINTSYS.SAPMiddleware.Payments
{
    public class PaymentAppServicecs : SAPMiddlewareAppServiceBase, IPaymentAppService
    {
        private readonly PaymentManager _paymentManager;

        public PaymentAppServicecs(PaymentManager paymentManager)
        {
            _paymentManager = paymentManager;
        }

        public Task CreatePayment(AddPaymentInput input)
        {
            var payment = ObjectMapper.Map<Payment>(input);

            return _paymentManager.CreatePayment(payment);
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
                LastErrorMessage = payment.LastErrorMessage,
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
