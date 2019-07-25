using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace GRINTSYS.SAPMiddleware.Clients.Dto
{
    [AutoMap(typeof(M2.Client))]
    public class ClientDto: EntityDto, IHasCreationTime, IMustHaveTenant
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

        public DateTime CreationTime { get; set; }

    }

}
