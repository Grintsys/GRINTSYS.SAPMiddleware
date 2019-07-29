using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using AutoMapper;
using GRINTSYS.SAPMiddleware.Cart;
using GRINTSYS.SAPMiddleware.Carts.Dto;
using GRINTSYS.SAPMiddleware.M2;
using GRINTSYS.SAPMiddleware.M2.Products;
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
        private ProductManager _productManager;

        public CartAppService(CartManager cartManager, ProductManager productManager)
        {
            this._cartManager = cartManager;
            this._productManager = productManager;
        }

        public async Task AddCart(AddCartInput input)
        {
            M2.Cart cart = Mapper.Map<AddCartInput, M2.Cart>(input);
            await _cartManager.CreateCart(cart);
        }

        public async Task AddItemToCart(AddCartItemInput input)
        {
            var productVariant = _productManager.GetProductVariant(input.CartProductVariantId);

            var cartProductVariantItem = new M2.CartProductVariant()
            {
                //CategoryId = productVariant.
            };

            await _cartManager.CreateCart(new M2.Cart(input.UserId));

            //await _cartManager.CreateCartProductVariant()
        }

        public async Task DeleteCart(DeleteCartInput input)
        {
            await _cartManager.DeleteCart((int)input.Id);
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
