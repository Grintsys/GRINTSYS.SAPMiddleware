﻿using Abp.Collections.Extensions;
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

        public CartManager(IRepository<Cart> cartRepository, 
            IRepository<CartProductItem> cartProductItemRepository)
        {
            this._cartRepository = cartRepository;
            this._cartProductItemRepository = cartProductItemRepository;
        }

        public async Task<Cart> CreateCart(Cart entity)
        {
            var cart =  _cartRepository.GetAllList()
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
            var cartItem = _cartProductItemRepository.GetAll()
                .Where(w => w.TenantId == entity.TenantId 
                    && w.ProductVariantId == entity.ProductVariantId
                    && w.CartId == entity.CartId)
                .FirstOrDefault();

            if(cartItem != null)
            {
                throw new UserFriendlyException("Item is Already on this Cart");
            }

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

        public Task DeleteUserCart(long userId, int tenantId)
        {
            var entity = _cartRepository.GetAll()
                .Where(w => w.UserId == userId
                    && w.TenantId == tenantId)
                .FirstOrDefault();

            if (entity == null)
            {
                throw new UserFriendlyException("user doesn't have a cart");
            }

            return _cartRepository.DeleteAsync(entity);
        }

        public Cart GetCart(int id)
        {
            return _cartRepository.Get(id);
        }

        public Cart GetCartByUser(long userId, int tenantId)
        {
            var cart = _cartRepository.GetAllIncluding(x => x.CartProductItems)
                .Where(w => w.UserId == userId 
                    && w.TenantId == tenantId
                    && w.Type == CartType.CART)
                .FirstOrDefault();

            return cart;
        }

        public CartProductItem GetCartProductItem(int id)
        {
            return _cartProductItemRepository.Get(id);
        }

        public List<CartProductItem> GetCartProductItems(int cartId)
        {
            return _cartProductItemRepository.GetAllIncluding(x => x.Variant)
                .Where(w => w.CartId == cartId)
                .ToList();
        }

        public List<CartProductItem> GetCartProductItemsByUser(long userId, int tenantId)
        {
            var cart = _cartRepository.GetAllIncluding(x => x.CartProductItems)
                .Where(w => w.UserId == userId
                    && w.TenantId == tenantId
                    && w.Type == CartType.CART)
                .FirstOrDefault();

            if(cart == null)
            {
                throw new UserFriendlyException("cart not found");
            }

            return _cartProductItemRepository.GetAllIncluding(x => x.Variant)
               .Where(w => w.CartId == cart.Id)
               .ToList();
        }
    }
}
