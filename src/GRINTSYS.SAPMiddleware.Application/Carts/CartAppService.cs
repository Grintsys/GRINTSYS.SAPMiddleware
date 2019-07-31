using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using AutoMapper;
using GRINTSYS.SAPMiddleware.Cart;
using GRINTSYS.SAPMiddleware.Carts.Dto;
using GRINTSYS.SAPMiddleware.M2;
using GRINTSYS.SAPMiddleware.M2.Clients;
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
        private ClientManager _clientManager;

        public CartAppService(CartManager cartManager,
            ProductManager productManager, 
            ClientManager clientManager)
        {
            this._cartManager = cartManager;
            this._productManager = productManager;
            this._clientManager = clientManager;
        }

        public async Task AddCart(AddCartInput input)
        {
            M2.Cart cart = Mapper.Map<AddCartInput, M2.Cart>(input);
            await _cartManager.CreateCart(cart);
        }

        public async Task AddItemToCart(AddCartItemInput input)
        {
            // get the product that we'll add to the cart
            var productVariant = _productManager.GetProductVariant(input.CartProductVariantId);

            // create the cart if not exits for the order
            var cart = await _cartManager.CreateCart(new M2.Cart(input.UserId));

            // create a copy from the product variant state (because we should mantain price and other stuff)
            var entity = new M2.CartProductVariant();
            entity.CopyFromProduct(productVariant);
                     
            await _cartManager.CreateCartProductVariant(entity);

            // Get the discount if that costumer apply
            var discount = _clientManager.GetClientDiscountAndItemGroupCode(input.CardCode, productVariant.ItemGroup);
            
            //Finally add the product Item
            await _cartManager.CreateCartProductItem(new M2.CartProductItem(cart.Id, input.Quantity, 0.15, discount == null ? 0 : discount.Discount));
        }

        public async Task DeleteCart(DeleteCartInput input)
        {
            await _cartManager.DeleteCart(input.Id);
        }

        public async Task DeleteItemToCart(DeleteCartItemInput input)
        {
            await _cartManager.DeleteCartProductItem(input.Id);
        }

        public Dto.CartOutput GetCart(GetCartInput input)
        {
            var cart = _cartManager.GetCartByUser(input.UserId, input.TenantId);

            return new Dto.CartOutput()
            {
                id = cart.Id,
                subtotal = cart.GetProductSubtotalPrice(),
                ISV = cart.GetProductISVPrice(),
                product_count = cart.GetProductCount(),
                currency = "NHL",
                discount = cart.GetProductDiscountPrice(),
                total_price = cart.GetProductTotalPrice(),
                items = cart.CartProductItems.ToList()
            };           
        }

        /*
        public CartOutput GetCart(GetCartInput input)
        {
            
            var cart = _cartManager.get
                .WhereIf(input.TenantId.HasValue, t => t.TenantId == input.TenantId.Value)
                .WhereIf(input.UserId.HasValue, t => t.UserId == input.UserId)
                .FirstOrDefault()
                ;
                
        }*/
    }
}
