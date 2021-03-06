﻿using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.M2
{
    public interface ICartManager: IDomainService
    {
        Cart GetCart(int id);
        Cart GetCartByUser(long userId, int tenantId);
        CartProductItem GetCartProductItem(int id);
        List<CartProductItem> GetCartProductItems(int cartId);
        List<CartProductItem> GetCartProductItemsByUser(long userId, int tenantId);
        Task<Cart> CreateCart(Cart entity);
        Task<CartProductItem> CreateCartProductItem(CartProductItem entity);
        Task DeleteCart(int id);
        Task DeleteUserCart(long userId, int tenantId);
        Task DeleteCartProductItem(int id);
    }
}
