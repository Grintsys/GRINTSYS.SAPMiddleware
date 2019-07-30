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
        CartProductItem GetCartProductItem(int id);
        CartProductVariant GetCartProductVariant(int id);
        Task<Cart> CreateCart(Cart entity);
        Task<CartProductItem> CreateCartProductItem(CartProductItem entity);
        Task<CartProductVariant> CreateCartProductVariant(CartProductVariant entity);
        Task DeleteCart(int id);
        Task DeleteCartProductVariant(int id);
        Task DeleteCartProductItem(int id);
    }
}
