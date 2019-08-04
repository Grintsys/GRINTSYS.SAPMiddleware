using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.ComponentModel.DataAnnotations;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class Bank : Entity, IHasCreationTime, IMustHaveTenant
    {
        public const int MaxNameLength = 256;
        public int TenantId { get; set; }
        [Required]
        [StringLength(MaxNameLength)]
        public String Name { get; set; }
        public String GeneralAccount { get; set; }
        public DateTime CreationTime { get; set; }
        public Bank()
        {
            CreationTime = Clock.Now;
        }
    }
}