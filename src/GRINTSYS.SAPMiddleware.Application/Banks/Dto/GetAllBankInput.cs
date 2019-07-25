using System;

namespace GRINTSYS.SAPMiddleware.Banks.Dto
{
    public class GetAllBankInput
    {
        public int? TenantId { get; set; }
        public String Name { get; set; }
    }
}
