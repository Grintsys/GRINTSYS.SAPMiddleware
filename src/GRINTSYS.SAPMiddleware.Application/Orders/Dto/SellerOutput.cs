using Abp.AutoMapper;
using GRINTSYS.SAPMiddleware.Authorization.Users;

namespace GRINTSYS.SAPMiddleware.Orders
{
    [AutoMapFrom(typeof(User))]
    public class SellerOutput
    {
        public long Id { get; set; }
        public string Name { get; set; } 
        public string LastName { get; set; }
        public int SalesPersonId { get; set; }
        public int CollectId { get; set; }
    }
}