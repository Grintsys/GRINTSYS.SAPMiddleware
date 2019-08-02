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

        public CartAppService(CartManager cartManager,
            ProductManager productManager)
        {
            this._cartManager = cartManager;
            this._productManager = productManager;
        }

        public long GetUserId()
        {
            var userId = long.MinValue;
            try
            {
                userId = AbpSession.GetUserId();
            }
            catch (Exception)
            {
                throw new UserFriendlyException("Expired Session");
            }

            return userId;
        }

        public async Task AddItemToCart(AddCartItemInput input)
        {
            var userId = GetUserId();

            // create the cart if not exits otherwise get the current cart
            var cart = await _cartManager.CreateCart(new Cart(input.TenantId, userId));

            // get the product that we'll add to the cart
            var productVariant = await _productManager.GetProductVariantAsync(input.ProductVariantId);

            //var tenant = this.TenantManager.FindByIdAsync(input.TenantId);
            var isv = await this.TenantManager.GetFeatureValueOrNullAsync(input.TenantId, "ISV");
            //var isv = tenant == null ? 0.0 : tenant.
            // Get the discount if that costumer apply
            //var discount = _clientManager.GetClientDiscountByItemGroupCode(input.CardCode, productVariant.ItemGroup);

            var cartItem = new CartProductItem(input.TenantId, cart.Id, productVariant.Id, input.Quantity, productVariant.Price, 0, 0);
            //add the item to the cart
            await _cartManager.CreateCartProductItem(cartItem);
        }

        public async Task DeleteCart(DeleteCartInput input)
        {
            await _cartManager.DeleteCart(input.Id);
        }

        public async Task DeleteItemToCart(DeleteCartItemInput input)
        {
            //var userId = GetUserId();
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

            return new CartOutput()
            {
                id = cart.Id,
                subtotal = items.Sum(c => c.Variant.Price * c.Quantity),
                ISV = cart.GetProductISVPrice(),
                product_count = cart.GetProductCount(),
                currency = "NHL",
                discount = cart.GetProductDiscountPrice(),
                total_price = (items.Sum(c => c.Variant.Price * c.Quantity) - cart.GetProductDiscountPrice()) + cart.GetProductISVPrice(),
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
    }
}
