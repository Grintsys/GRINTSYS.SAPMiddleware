using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.M2.Payments
{
    public interface IPaymentManager : IDomainService
    {
        Task CreatePayment(Payment payment);
        Task AddCashPayment(PaymentCash paymentCash);
        Task AddCheckPayment(PaymentCheck paymentCheck);
        Task AddTransferPayment(PaymentTransfer paymentTransfer);
    }
}
