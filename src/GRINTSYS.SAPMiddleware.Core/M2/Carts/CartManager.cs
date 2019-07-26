using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.M2
{
    public class CartManager : DomainService, ICartManager
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<CartProductItem> _cartProductItemRepository;
        private readonly IRepository<CartProductVariant> _cartProductVariantRepository;

        public CartManager(IRepository<Cart> cartRepository, 
            IRepository<CartProductItem> cartProductItemRepository,
            IRepository<CartProductVariant> cartProductVariantRepository)
        {
            this._cartRepository = cartRepository;
            this._cartProductItemRepository = cartProductItemRepository;
            this._cartProductVariantRepository = cartProductVariantRepository;
        }

        public async Task<Cart> CreateCart(Cart entity)
        {
            return await _cartRepository.InsertAsync(entity);
        }

        public async Task<CartProductItem> CreateCartProductVariant(CartProductItem entity)
        {
            return await _cartProductItemRepository.InsertAsync(entity);
        }

        public async Task DeleteCart(int id)
        {
            var entity = _cartRepository.Get(id);

            if (entity == null)
            {
                throw new UserFriendlyException("Cart not found");
            }

            await _cartRepository.DeleteAsync(entity);
        }

        public async Task DeleteCartProductItem(int id)
        {
            var entity = _cartProductItemRepository.Get(id);

            if (entity == null)
            {
                throw new UserFriendlyException("Item not found");
            }

            await _cartProductItemRepository.DeleteAsync(entity);
        }

        public async Task DeleteCartProductVariant(int id)
        {
            var entity = _cartProductVariantRepository.Get(id);

            if (entity == null)
            {
                throw new UserFriendlyException("Item not found");
            }

            await _cartProductVariantRepository.DeleteAsync(entity);
        }

        public Cart GetCart(int id)
        {
            return _cartRepository.Get(id);
        }

        public CartProductItem GetCartProductItem(int id)
        {
            throw new NotImplementedException();
        }

        public CartProductItem GetCartProductVariant(int id)
        {
            throw new NotImplementedException();
        }
    }
}
