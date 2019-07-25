using Abp.Application.Services;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GRINTSYS.SAPMiddleware.Authorization;
using GRINTSYS.SAPMiddleware.Banners.Dto;
using GRINTSYS.SAPMiddleware.M2;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Banners
{
    [AbpAuthorize(PermissionNames.Pages_MobileAccess)]
    public class BannerAppService : AsyncCrudAppService<Banner, BannerDto, int, GetAllBannerInput>, IBannerAppService
    {
        public BannerAppService(IRepository<Banner> respository)
           : base(respository)
        {
            CreatePermissionName = PermissionNames.Pages_MobileAccess;
        }

        public override async Task<BannerDto> Create(BannerDto input)
        {
            CheckCreatePermission();

            var obj = ObjectMapper.Map<Banner>(input);

            //CheckErrors(await _roleManager.CreateAsync(role));

            await this.Repository.InsertAsync(obj);

            return MapToEntityDto(obj);
        }

        protected override IQueryable<Banner> CreateFilteredQuery(GetAllBannerInput input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(input.TenantId.HasValue, t => t.TenantId == input.TenantId.Value)
                .WhereIf(!String.IsNullOrEmpty(input.Name), t => t.Name.Contains(input.Name))
                ;
        }
    }
}
