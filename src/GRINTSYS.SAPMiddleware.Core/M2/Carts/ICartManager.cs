using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.M2
{
    public interface ICartManager: IDomainService
    {
        Cart GetCart(int id);
        Task<Cart> CreateCart(Cart entity);
        Task DeleteCart(int id);
        CartProductItem GetCartProductVariant(int id);
        Task<CartProductItem> CreateCartProductVariant(CartProductItem entity);
        Task DeleteCartProductVariant(int id);
        CartProductItem GetCartProductItem(int id);
        Task DeleteCartProductItem(int id);
    }
}
