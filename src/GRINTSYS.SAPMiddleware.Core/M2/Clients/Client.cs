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

        [JsonProperty(PropertyName = "name")]
        public String Name { get; set; }
        [JsonProperty(PropertyName = "card_code")]
        public String CardCode { get; set; }
        [JsonProperty(PropertyName = "phone")]
        public String PhoneNumber { get; set; }
        [JsonProperty(PropertyName = "address")]
        public Double CreditLimit { get; set; }
        [JsonProperty(PropertyName = "credit_limit")]
        public Double Balance { get; set; }
        [JsonProperty(PropertyName = "balance")]
        public Double InOrders { get; set; }
        [JsonProperty(PropertyName = "in_oders")]
        public String PayCondition { get; set; }
        [JsonProperty(PropertyName = "pay_condition")]
        public String Address { get; set; }
        [JsonProperty(PropertyName = "discount_percent")]
        public String RTN { get; set; }
        [JsonProperty(PropertyName = "past_due")]
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
