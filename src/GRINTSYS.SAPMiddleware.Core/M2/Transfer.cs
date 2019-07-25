using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace GRINTSYS.SAPMiddleware.M2
{
    public class Transfer: Entity, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public String ReferenceNumber { get; set; }
        public Double Amount { get; set; }
        public DateTime Date { get; set; }
        public String GeneralAccount { get; set; }

        public DateTime CreationTime { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public Transfer()
        {
            CreationTime = Clock.Now;
        }
    }
}