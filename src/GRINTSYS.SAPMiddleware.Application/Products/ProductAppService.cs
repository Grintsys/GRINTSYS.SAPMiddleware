using Abp.Application.Services;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GRINTSYS.SAPMiddleware.Authorization;
using GRINTSYS.SAPMiddleware.Authorization.Roles;
using GRINTSYS.SAPMiddleware.M2;
using GRINTSYS.SAPMiddleware.M2.Products;
using GRINTSYS.SAPMiddleware.Products.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Products
{
    [AbpAuthorize(PermissionNames.Pages_MobileAccess)]
    public class ProductAppService : SAPMiddlewareAppServiceBase, IProductAppService
    {
        private readonly ProductManager _productManager;
        //ProductManager productManager;

        public ProductAppService(ProductManager productManager) 
        {
            _productManager = productManager;
        }

        public async Task CreateProduct(AddProductInput input)
        {
            var product = ObjectMapper.Map<Product>(input);
            await _productManager.CreateProduct(product);
        }

        public async Task CreateVariant(AddProductVariantInput input)
        {
            var productVariant = ObjectMapper.Map<ProductVariant>(input);
            await _productManager.CreateProductVariant(productVariant);
        }

        public List<ProductOutput> GetAllProducts(GetAllProductInput input)
        {
            throw new System.NotImplementedException();
        }

        public ProductOutput GetProduct(GetProductInput input)
        {
            var product = _productManager.GetProduct(input.Id);

            var result = ObjectMapper.Map<ProductOutput>(product);

            return result;
        }
    }
}
