using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class ReportEntry: Entity, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [JsonProperty(PropertyName = "x")]
        public Double X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public Double Y { get; set; }
        [JsonProperty(PropertyName = "label")]
        public String Label { get; set; }
        [JsonProperty(PropertyName = "index")]
        public Int32 Index { get; set; }
    }
}
