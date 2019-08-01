using Abp.Application.Services;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using GRINTSYS.SAPMiddleware.Authorization;
using GRINTSYS.SAPMiddleware.M2;
using System;
using System.Linq;
using Abp.Linq.Extensions;
using System.Threading.Tasks;
using GRINTSYS.SAPMiddleware.Products.Dto;
using System.Collections.Generic;
using GRINTSYS.SAPMiddleware.M2.Products;
using AutoMapper;

namespace GRINTSYS.SAPMiddleware.Products
{
    [AbpAuthorize(PermissionNames.Pages_MobileAccess)]
    public class ProductAppService : ApplicationService, IProductAppService
    {
        private readonly ProductManager _productManager;

        public ProductAppService(ProductManager productManager)
        {
            _productManager = productManager;
        }

        public async Task CreateProduct(AddProductInput input)
        {
            var product = Mapper.Map<AddProductInput, Product>(input);
            await _productManager.CreateProduct(product);
        }

        public async Task CreateProductVariant(AddProductVariantInput input)
        {
            var productVariant = Mapper.Map<AddProductVariantInput, ProductVariant>(input);
            await _productManager.CreateProductVariant(productVariant);
        }

        public ProductDto GetProduct(GetProductInput input)
        {
            throw new NotImplementedException();
        }
    }
}
