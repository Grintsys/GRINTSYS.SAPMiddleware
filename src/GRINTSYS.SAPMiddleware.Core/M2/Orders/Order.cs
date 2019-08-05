using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GRINTSYS.SAPMiddleware.M2
{
    public enum OrderStatus
    {
        CreadoEnAplicacion = 0,
        PreliminarEnSAP = 1,
        Autorizado = 2,
        ErrorAlCrearEnSAP = 3
    }

    public class Order: Entity, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public String RemoteId { get; set; }
        public OrderStatus Status { get; set; }
        public String CardCode { get; set; }
        public long UserId { get; set; }
        public String Comment { get; set; }
        public String LastMessage { get; set; }
        public Int32 Series { get; set; }
        public String DeliveryDate { get; set; }

        public DateTime CreationTime { get; set; }

        public Double GetTotal()
        {
            return (GetSubtotal() - GetDiscount()) + GetIVA();
        }

        public Double GetItemCount()
        {
            return this.OrderItems.Count;
        }

        public Double GetSubtotal()
        {
            return this.OrderItems.Sum(s => s.Quantity * s.Price);
        }

        public Double GetIVA()
        {
            return this.OrderItems.Sum(s => ((s.Quantity * s.Price) - s.Discount) * s.TaxValue);
        }

        public Double GetDiscount()
        {
            return this.OrderItems.Sum(s => s.Discount);
        }

        public Order()
        {
            RemoteId = "";
            Comment = "";
            LastMessage = "";
            Series = 0;
            CreationTime = Clock.Now;
        }

        public virtual User User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}