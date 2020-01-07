using GRINTSYS.SAPMiddleware.Clients.Dto;
using GRINTSYS.SAPMiddleware.Payments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Web.Models.Payments
{
    public class EditPaymentModalViewModel
    {
        public PaymentOutput Payment { get; set; }
        public IReadOnlyList<PaymentItemOutput> PaymentItems { get; set; }
        public IReadOnlyList<InvoiceDto> ClientInvoices { get; set; }
    }
}
