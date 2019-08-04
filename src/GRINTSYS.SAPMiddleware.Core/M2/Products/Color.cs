using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.ComponentModel.DataAnnotations;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class Color: Entity, IHasCreationTime, IMustHaveTenant
    {
        public const int MaxDescriptionLength = 64 * 1024; //64KB
        public int TenantId { get; set; }
        public Int32 RemoteId { get; set; }
        public String Value { get; set; }
        public String Code { get; set; }
        public String Image { get; set; }
        [StringLength(MaxDescriptionLength)]
        public String Description { get; set; }
        public DateTime CreationTime { get; set; }

        public Color()
        {
            CreationTime = Clock.Now;
            Code = "#000000";
            RemoteId = 0;
        }
    }
}
