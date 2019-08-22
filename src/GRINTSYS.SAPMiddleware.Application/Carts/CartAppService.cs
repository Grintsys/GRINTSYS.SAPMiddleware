using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using GRINTSYS.SAPMiddleware.Carts.Dto;
using GRINTSYS.SAPMiddleware.M2;
using GRINTSYS.SAPMiddleware.M2.Clients;
using GRINTSYS.SAPMiddleware.M2.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Carts
{
    //[AbpAuthorize(PermissionNames.Pages_MobileUsers)]
    public class CartAppService : SAPMiddlewareAppServiceBase, ICartAppService
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

        public async Task AddItemToCart(AddCartItemInput input)
        {
            var userId = GetUserId();

            //here we have some global settings
            var tenant = await GetCurrentTenantAsync();

            // create the cart if not exits otherwise get the current cart
            var cart = await _cartManager.CreateCart(new Cart(input.TenantId, tenant.Currency, userId));

            // get the product that we'll add to the cart
            var productVariant = await _productManager.GetProductVariantAsync(input.ProductVariantId);

            var discount = _clientManager.GetClientDiscountByItemGroupCode(input.CardCode, productVariant.ItemGroup);

            var cartItem = new CartProductItem(input.TenantId, cart.Id, productVariant.Id, input.Quantity, productVariant.Price, tenant.ISV, discount);            
            //finally add the item to the cart
            await _cartManager.CreateCartProductItem(cartItem);
        }

        public async Task DeleteCart(DeleteCartInput input)
        {
            await _cartManager.DeleteCart(input.Id);
        }

        public async Task DeleteItemToCart(DeleteCartItemInput input)
        {
            //var userId = GetUserId();
            //TODO: another user can delete cart items
            await _cartManager.DeleteCartProductItem(input.Id);
        }

        public CartOutput GetCart(GetCartInput input)
        {
            var userId = AbpSession.GetUserId();
            //var tenant = await GetCurrentTenantAsync();

            var cart = _cartManager.GetCartByUser(userId, input.TenantId);

            if(cart == null)
            {
                return new CartOutput(-1);
            }

            var items = _cartManager.GetCartProductItems(cart.Id);

            var currency = cart.CartProductItems.FirstOrDefault();
            var total = (items.Sum(c => c.Variant.Price * c.Quantity) - cart.GetProductDiscountPrice()) + cart.GetProductISVPrice();
            return new CartOutput()
            {
                Id = cart.Id,
                Subtotal = items.Sum(c => c.Variant.Price * c.Quantity),
                ISV = cart.GetProductISVPrice(),
                ProductCount = cart.GetProductCount(),
                Currency = cart.Currency,
                Discount = cart.GetProductDiscountPrice(),
                TotalPrice = total,
                TotalPriceFormatted = cart.Currency + " " + total,
                items = items.MapTo<List<CartProductItemOutput>>()
            };
        }

        public CartOutput GetCartInfo(GetCartInput input)
        {
            throw new System.NotImplementedException();
        }

        public double CalculateSubtotal(int cartId)
        {
            var items = _cartManager.GetCartProductItems(cartId);

            return items.Sum(s => s.Variant.Price * s.Quantity);
        }

        public Task CreateCart(AddCartInput input)
        {
            var userId = GetUserId();
            return _cartManager.CreateCart(new Cart(input.TenantId, "", userId));
        }
    }
}
