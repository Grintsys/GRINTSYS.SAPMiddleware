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

namespace GRINTSYS.SAPMiddleware.Products
{
    [AbpAuthorize(PermissionNames.Pages_MobileAccess)]
    public class ProductAppService : AsyncCrudAppService<Product, ProductDto, int, GetAllProductInput>, IProductAppService
    {
        public ProductAppService(IRepository<Product> productRespository)
           : base(productRespository)
        {
            CreatePermissionName = PermissionNames.Pages_MobileAccess;
        }

        public override async Task<ProductDto> Create(ProductDto input)
        {
            CheckCreatePermission();

            var product = ObjectMapper.Map<Product>(input);

            //CheckErrors(await _roleManager.CreateAsync(role));

            await this.Repository.InsertAsync(product);

            return MapToEntityDto(product);
        }

        protected override IQueryable<Product> CreateFilteredQuery(GetAllProductInput input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(input.TenantId.HasValue, t => t.TenantId == input.TenantId.Value)
                .WhereIf(input.CategoryId.HasValue, t => t.CategoryId == input.CategoryId.Value)
                .WhereIf(input.BrandId.HasValue, t => t.BrandId == input.CategoryId.Value)
                .WhereIf(!String.IsNullOrEmpty(input.Name), t => t.Name.Contains(input.Name))
                .WhereIf(!String.IsNullOrEmpty(input.Code), t => t.Name.Contains(input.Code))
                .WhereIf(!String.IsNullOrEmpty(input.Description), t => t.Name.Contains(input.Description))
                ;
        }
    }
}
