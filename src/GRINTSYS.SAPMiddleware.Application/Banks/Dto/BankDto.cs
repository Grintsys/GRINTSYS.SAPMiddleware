using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace GRINTSYS.SAPMiddleware.Banks.Dto
{
    [AutoMap(typeof(M2.Bank))]
    public class BankDto: EntityDto, IHasCreationTime, IMustHaveTenant
    {
        public const int MaxNameLength = 256;
        public int TenantId { get; set; }
        [Required]
        [StringLength(MaxNameLength)]
        public String Name { get; set; }
        public String FormatCode { get; set; }
        public String GeneralAccount { get; set; }
        public DateTime CreationTime { get; set; }
    }

}
