using System;

namespace GRINTSYS.SAPMiddleware.Colors.Dto
{
    public class GetAllColorInput
    {
        public int? TenantId { get; set; }
        public String Value { get; set; }
        public String Code { get; set; }
    }
}
