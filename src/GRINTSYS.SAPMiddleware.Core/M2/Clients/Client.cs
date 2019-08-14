using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class Client: Entity, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public String Name { get; set; }
        public String CardCode { get; set; }
        public String PhoneNumber { get; set; }
        public Double CreditLimit { get; set; }
        public Double Balance { get; set; }
        public Double InOrders { get; set; }
        public String PayCondition { get; set; }
        public String Address { get; set; }
        public String RTN { get; set; }
        public Double PastDue { get; set; }
        public String ContactPerson { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }

        public DateTime CreationTime { get; set; }

        public Client()
        {
            CreationTime = Clock.Now;
        }
    }
}
