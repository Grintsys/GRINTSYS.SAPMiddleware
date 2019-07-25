using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GRINTSYS.SAPMiddleware
{
    [Table("M2Sizes")]
    public class Size : Entity, IHasCreationTime, IMustHaveTenant
    {
        public const int MaxValueLength = 24;
        public const int MaxDescriptionLength = 64 * 1024; //64KB

        public int TenantId { get; set; }

        [Required]
        [StringLength(MaxValueLength)]
        public String Value { get; set; }
        [StringLength(MaxDescriptionLength)]
        public String Description { get; set; }

        public DateTime CreationTime { get; set; }

        public Size()
        {
            CreationTime = Clock.Now;
            Value = "#000000";
        }

        public Size(string value, string description = null)
            : this()
        {
            Value = value;
            Description = description;
        }
    }
}
