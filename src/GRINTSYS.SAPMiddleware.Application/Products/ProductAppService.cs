using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GRINTSYS.SAPMiddleware.Authorization;
using GRINTSYS.SAPMiddleware.M2;
using GRINTSYS.SAPMiddleware.M2.Products;
using GRINTSYS.SAPMiddleware.Products.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
//using System.Linq;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Products
{
    [AbpAuthorize(PermissionNames.Pages_MobileAccess)]
    public class ProductAppService : SAPMiddlewareAppServiceBase, IProductAppService
    {
        private readonly IRepository<Product> _productRepository;
        //private readonly IRepository<ProductVariant> _productVariantRespository;
        private readonly ProductManager _productManager;

        public ProductAppService(IRepository<Product> productRepository, ProductManager productManager) 
        {
            _productRepository = productRepository;
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

        public PagedResultDto<ProductOutput> GetAllProducts(GetAllProductInput input)
        {
            if (input.MaxResultCount <= 0)
                input.MaxResultCount = AppConsts.MaxResultCount;

            if (String.IsNullOrEmpty(input.Sorting))
                input.Sorting = AppConsts.DefaultSortingField;

            var productsCount = _productRepository.Count();
            var products = _productRepository.GetAllIncluding(x => x.Variants)
                .WhereIf(input.TenantId.HasValue, t => t.TenantId == input.TenantId.Value)
                .WhereIf(input.CategoryId.HasValue, t => t.CategoryId == input.CategoryId.Value)
                .WhereIf(input.BrandId.HasValue, t => t.BrandId == input.BrandId.Value)
                .WhereIf(!String.IsNullOrEmpty(input.Name), t => t.Name.Contains(input.Name))
                .WhereIf(!String.IsNullOrEmpty(input.Code), t => t.Code.Contains(input.Code))
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToList()
                ;

            return new PagedResultDto<ProductOutput>
            {
                TotalCount = productsCount,
                Items = products.MapTo<List<ProductOutput>>()
            };
        }

        public ProductOutput GetProduct(GetProductInput input)
        {
            var product = _productManager.GetProduct(input.Id);

            var result = ObjectMapper.Map<ProductOutput>(product);

            return result;
        }
    }
}
