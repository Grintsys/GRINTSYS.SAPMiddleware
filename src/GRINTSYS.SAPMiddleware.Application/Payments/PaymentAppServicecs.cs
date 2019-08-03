using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        public Task CreatePayment(PaymentInput input)
        {
            var payment = new Payment()
            {
                TenantId = input.TenantId,
                TotalAmount = input.Total,
                Comment = input.Comment
            };

            return _paymentManager.CreatePayment(payment);
        }

        public Task PayByCash(CashInput input)
        {
            throw new NotImplementedException();
        }

        public Task PayByCheck(CheckInput input)
        {
            throw new NotImplementedException();
        }

        public Task PayByTransfer(TransferInput input)
        {
            throw new NotImplementedException();
        }
    }
}
