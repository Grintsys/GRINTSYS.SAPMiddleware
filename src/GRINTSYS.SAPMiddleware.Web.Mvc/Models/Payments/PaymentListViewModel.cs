using GRINTSYS.SAPMiddleware.Payments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Web.Models.Payments
{
    public class PaymentListViewModel
    {
        public IReadOnlyList<PaymentOutput> Payments { get; set; }
    }
}
