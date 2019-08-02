using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GRINTSYS.SAPMiddleware.Payments.Dto;

namespace GRINTSYS.SAPMiddleware.Payments
{
    public class PaymentAppServicecs : SAPMiddlewareAppServiceBase, IPaymentAppService
    {
        public Task CreatePayment(PaymentInput payment)
        {
            throw new NotImplementedException();
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
