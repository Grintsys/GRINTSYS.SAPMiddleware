using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Linq.Extensions;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var cart = _cartRepository.GetAll()
                .Where(w => w.UserId == entity.UserId)
                .FirstOrDefault();

            if(cart == null)
            {
                return await _cartRepository.InsertAsync(entity);
            }

            return cart;
        }

        public async Task<CartProductItem> CreateCartProductItem(CartProductItem entity)
        {
            return await _cartProductItemRepository.InsertAsync(entity);
        }

        public async Task<CartProductVariant> CreateCartProductVariant(CartProductVariant entity)
        {
            return await _cartProductVariantRepository.InsertAsync(entity);
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

        public Cart GetCartByUser(int userId, int tenantId)
        {
            var cart = _cartRepository.GetAll()
                .Where(w => w.UserId == userId 
                    && w.TenantId == tenantId
                    && w.Type == CartType.CART)
                .FirstOrDefault();

            if(cart == null)
            {
                return new Cart(-1);
            }

            return cart;
        }

        public CartProductItem GetCartProductItem(int id)
        {
            return _cartProductItemRepository.Get(id);
        }

        public CartProductVariant GetCartProductVariant(int id)
        {
            return _cartProductVariantRepository.Get(id);
        }
    }
}
