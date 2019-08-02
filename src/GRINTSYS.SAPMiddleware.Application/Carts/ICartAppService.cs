using Abp.Application.Services;
using Abp.Dependency;
using GRINTSYS.SAPMiddleware.Carts.Dto;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Carts
{
    public interface ICartAppService : IApplicationService, ITransientDependency
    {
        CartOutput GetCart(GetCartInput input);
        CartOutput GetCartInfo(GetCartInput input);
        Task DeleteCart(DeleteCartInput input);
        Task AddItemToCart(AddCartItemInput input);
        Task DeleteItemToCart(DeleteCartItemInput input);
        double CalculateSubtotal(int cartId);
    }
}
