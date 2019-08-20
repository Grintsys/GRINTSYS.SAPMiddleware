using GRINTSYS.SAPMiddleware.Orders.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Web.Models.Orders
{
    public class OrderListViewModel
    {
        public IReadOnlyList<OrderOutput> Orders { get; set; }
    }
}
