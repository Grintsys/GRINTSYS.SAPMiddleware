using Abp.Application.Services;
using GRINTSYS.SAPMiddleware.Carts.Dto;

namespace GRINTSYS.SAPMiddleware.Cart
{
    public interface ICartAppService: IAsyncCrudAppService<CartDto, int, GetAllUserCartInput>
    {
    }
}
