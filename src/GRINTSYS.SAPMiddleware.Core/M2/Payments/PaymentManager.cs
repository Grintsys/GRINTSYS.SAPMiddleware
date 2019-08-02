using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.M2.Payments
{
    public class PaymentManager : DomainService, IPaymentManager
    {
        private readonly IRepository<Payment> _paymentRepository;
        private readonly IRepository<PaymentCash> _paymentCashRepository;
        private readonly IRepository<PaymentTransfer> _paymentTransferRepository;
        private readonly IRepository<PaymentCheck> _paymentCheckRepository;

        public PaymentManager(IRepository<Payment> paymentRepository,
            IRepository<PaymentCash> paymentCashRepository,
            IRepository<PaymentTransfer> paymentTransferRepository,
            IRepository<PaymentCheck> paymentCheckRepository)
        {
            _paymentRepository = paymentRepository;
            _paymentCashRepository = paymentCashRepository;
            _paymentTransferRepository = paymentTransferRepository;
            _paymentCheckRepository = paymentCheckRepository;
        }

        public Task AddCashPayment(PaymentCash paymentCash)
        {
            return _paymentCashRepository.InsertAsync(paymentCash);
        }

        public Task AddCheckPayment(PaymentCheck paymentCheck)
        {
            return _paymentCheckRepository.InsertAsync(paymentCheck);
        }

        public Task AddTransferPayment(PaymentTransfer paymentTransfer)
        {
            return _paymentTransferRepository.InsertAsync(paymentTransfer);
        }
    }
}
