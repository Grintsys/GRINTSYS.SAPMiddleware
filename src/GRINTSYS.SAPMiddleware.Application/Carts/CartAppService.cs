using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using AutoMapper;
using GRINTSYS.SAPMiddleware.Cart;
using GRINTSYS.SAPMiddleware.Carts.Dto;
using GRINTSYS.SAPMiddleware.M2;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Carts
{
    //[AbpAuthorize(PermissionNames.Pages_MobileUsers)]
    public class CartAppService : ApplicationService, ICartAppService
    {
        private CartManager _cartManager;

        public CartAppService(CartManager cartManager)
        {
            this._cartManager = cartManager;
        }

        public async Task AddCart(AddCartInput input)
        {
            M2.Cart cart = Mapper.Map<AddCartInput, M2.Cart>(input);
            await _cartManager.CreateCart(cart);
        }

        public async Task AddItemToCart(AddCartItemInput input)
        {
            //await _cartProductItemManager.Get(input.)

            throw new NotImplementedException();
        }

        public Task DeleteCart(DeleteCartInput input)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItemToCart(DeleteCartInput input)
        {
            throw new NotImplementedException();
        }

        public CartOutput GetCart(GetCartInput input)
        {
            throw new NotImplementedException();
        }
    }
}
