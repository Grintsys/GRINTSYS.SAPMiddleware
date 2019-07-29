using GRINTSYS.SAPMiddleware.M2;

namespace GRINTSYS.SAPMiddleware.Carts.Dto
{
    public class DeleteCartInput
    {
        public int? Id { get; set; }
        public int? TenantId { get; set; }
        public int? UserId { get; set; }
        public CartType? Type { get; set; }
        public int ProductId { get; set; }
    }
}
