using Abp.Application.Services;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using GRINTSYS.SAPMiddleware.Authorization;
using GRINTSYS.SAPMiddleware.Brands.Dto;
using GRINTSYS.SAPMiddleware.M2;
using System;
using System.Linq;
using Abp.Linq.Extensions;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Brands
{
    [AbpAuthorize(PermissionNames.Pages_MobileAccess)]
    public class BrandAppService : AsyncCrudAppService<Brand, BrandDto, int, GetAllBrandInput>, IBrandAppService
    {
        public BrandAppService(IRepository<Brand> brandRespository)
           : base(brandRespository)
        {
            CreatePermissionName = PermissionNames.Pages_MobileAccess;
        }

        public override async Task<BrandDto> Create(BrandDto input)
        {
            CheckCreatePermission();

            var brand = ObjectMapper.Map<Brand>(input);

            //CheckErrors(await _roleManager.CreateAsync(role));

            await this.Repository.InsertAsync(brand);

            return MapToEntityDto(brand);
        }

        protected override IQueryable<Brand> CreateFilteredQuery(GetAllBrandInput input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(input.TenantId.HasValue, t => t.TenantId == input.TenantId.Value)
                .WhereIf(!String.IsNullOrEmpty(input.Name), t => t.Name.Contains(input.Name));
        }
    }
}
