using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.M2.Payments
{
    public class PaymentManager : DomainService, IPaymentManager
    {
        private readonly IRepository<Payment> _paymentRepository;
        private readonly IRepository<Invoice> _invoiceRepository;
        private readonly IRepository<PaymentInvoiceItem> _paymentInvoiceItemRepository;

        public PaymentManager(IRepository<Payment> paymentRepository,
            IRepository<Invoice> invoiceRepository,
            IRepository<PaymentInvoiceItem> paymentInvoiceItem)
        {
            _paymentRepository = paymentRepository;
            _invoiceRepository = invoiceRepository;
            _paymentInvoiceItemRepository = paymentInvoiceItem;
        }

        public Task CreateInvoice(Invoice invoice)
        {
            return _invoiceRepository.InsertAsync(invoice);
        }

        public Task<int> CreatePayment(Payment payment)
        {
            return _paymentRepository.InsertAndGetIdAsync(payment);
        }

        public Task<Payment> UpdatePaymentAsync(Payment payment)
        {
            return _paymentRepository.UpdateAsync(payment);
        }

        public Task DeletePayment(int id)
        {
            return _paymentRepository.DeleteAsync(id);
        }

        public Invoice GetInvoice(int id)
        {
            return _invoiceRepository.GetAllIncluding(x => x.Client)
                .Where(w => w.Id == id)
                .FirstOrDefault();
        }

        public Payment GetPayment(int id)
        {
            var payment = _paymentRepository.GetAllIncluding(x => x.Bank, x => x.User, x => x.InvoicesItems)
                .Where(w => w.Id == id)
                .FirstOrDefault();

            if (payment == null)
                throw new UserFriendlyException("payment not  found");

            return payment;
        }

        public List<Payment> GetPaymentsByUser(int tenantId, long userId, DateTime? begin, DateTime? end)
        {
            return _paymentRepository.GetAllIncluding(x => x.Bank, x=> x.User)
                .Where( w => w.TenantId == tenantId
                    && w.UserId == userId)
                .WhereIf(begin.HasValue && end.HasValue,  w => w.CreationTime >= begin && w.CreationTime <= end)
                .ToList();
        }

        public List<Payment> GetPayments(int tenantId, DateTime? begin, DateTime? end)
        {
            return _paymentRepository.GetAllIncluding(x => x.Bank, x=> x.InvoicesItems, x=> x.User)
                .Where(w => w.TenantId == tenantId)
                .WhereIf(begin.HasValue && end.HasValue, w => w.CreationTime >= begin && w.CreationTime <= end)
                .OrderByDescending( o => o.Id)
                .ToList();
        }

        public Payment UpdatePayment(Payment payment)
        {
            return _paymentRepository.Update(payment);
        }

        public void ValidatePayedAmount(int invoiceId, double amount)
        {
            var invoice = _invoiceRepository.FirstOrDefault(x => x.Id == invoiceId);

            if (invoice == null)
                throw new UserFriendlyException("Sorry but the amount can't be validated because invoice don't exist");

            if (amount > invoice.BalanceDue)
                throw new UserFriendlyException(String.Format("The payedAmount {0} is greatest than invoice due {1}", amount, invoice.BalanceDue));

            if (amount <= 0)
                throw new UserFriendlyException("the amount can't be 0 or less");
        }

        public Task AddPaymentInvoiceItem(PaymentInvoiceItem item)
        {
            return _paymentInvoiceItemRepository.InsertAsync(item);
        }
    }
}
