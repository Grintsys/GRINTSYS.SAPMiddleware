using GRINTSYS.SAPMiddleware.M2;

namespace GRINTSYS.SAPMiddleware.Carts.Dto
{
    public class GetAllUserCartInput
    {
        public int? UserId { get; set; }
        public CartType? Type { get; set; }
    }
}
