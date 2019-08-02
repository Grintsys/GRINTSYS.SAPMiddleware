using Abp.Runtime.Session;
using Abp.UI;
using GRINTSYS.SAPMiddleware.Carts.Dto;
using GRINTSYS.SAPMiddleware.M2;
using GRINTSYS.SAPMiddleware.M2.Clients;
using GRINTSYS.SAPMiddleware.M2.Products;
using System;
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
            var productVariant = _productManager.GetProductVariant(input.ProductVariantId);

            // Get the discount if that costumer apply
            var discount = _clientManager.GetClientDiscountByItemGroupCode(input.CardCode, productVariant.ItemGroup);

            var cartItem = new CartProductItem(input.TenantId, cart.Id, productVariant.Id, input.Quantity, 0.15, discount);
            //add the item to the cart
            await _cartManager.CreateCartProductItem(cartItem);
        }

        public async Task DeleteCart(DeleteCartInput input)
        {
            await _cartManager.DeleteCart(input.Id);
        }

        public async Task DeleteItemToCart(DeleteCartItemInput input)
        {
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

            return new CartOutput()
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

        public CartOutput GetCartInfo(GetCartInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
