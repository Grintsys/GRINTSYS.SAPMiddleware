using Abp.Application.Services;
using GRINTSYS.SAPMiddleware.Payments.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Payments
{
    public interface IPaymentAppService : IApplicationService
    {
        Task CreatePayment(PaymentInput payment);
        Task PayByCheck(CheckInput input);
        Task PayByTransfer(TransferInput input);
        Task PayByCash(CashInput input);
    }
}
