using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.M2.Payments
{
    public interface IPaymentManager : IDomainService
    {
        Task CreatePayment(Payment payment);
        Task AddPaymentInvoiceItem(PaymentInvoiceItem item);
        Task CreateInvoice(Invoice invoice);
        Task DeletePayment(int id);
        Payment GetPayment(int id);
        Payment UpdatePayment(Payment payment);
        Invoice GetInvoice(int id);
        List<Payment> GetPaymentsByUser(int tenantId, long userId, DateTime? begin, DateTime? end);
        void ValidatePayedAmount(int invoiceId, double amount);
    }
}
