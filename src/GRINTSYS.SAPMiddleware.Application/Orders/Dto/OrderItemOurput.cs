using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace GRINTSYS.SAPMiddleware.Orders.Dto
{
    [AutoMap(typeof(M2.OrderItem))]
    public class OrderItemOutput: EntityDto
    {
        public String Code { get; set; }
        public String Name { get; set; }
        public Int32 Quantity { get; set; }
        public Double Price { get; set; }
        public Double Discount { get; set; }
        public Double DiscountPercent { get; set; }
        public Double TaxValue { get; set; }
        public String TaxCode { get; set; }
        public String WarehouseCode { get; set; }
    }
}
