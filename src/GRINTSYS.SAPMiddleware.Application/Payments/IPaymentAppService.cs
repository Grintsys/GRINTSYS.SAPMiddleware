using Abp.Application.Services;
using GRINTSYS.SAPMiddleware.M2;
using GRINTSYS.SAPMiddleware.Payments.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Payments
{
    public interface IPaymentAppService : IApplicationService
    {
        Task CreatePayment(AddPaymentInput input);
        Task CreateInvoice(AddInvoiceInput input);
        PaymentOutput GetPayment(GetPaymentInput input);
        List<Payment> GetPaymentsByUser(GetAllPaymentInput input);
        Task AutorizePayment(GetPaymentInput input);
        PaymentOutput DeclinePayment(GetPaymentInput input);
    }
}
