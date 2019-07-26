using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GRINTSYS.SAPMiddleware.Carts.Dto;
using System;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Cart
{
    public interface ICartAppService : IApplicationService
    {
        CartOutput GetCart(GetCartInput input);
        Task AddCart(AddCartInput input);
        Task DeleteCart(DeleteCartInput input);
        Task AddItemToCart(AddCartItemInput input);
        Task DeleteItemToCart(DeleteCartInput input);
    }
}
