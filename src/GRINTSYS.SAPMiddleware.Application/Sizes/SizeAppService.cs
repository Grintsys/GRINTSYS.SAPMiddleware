using Abp.Application.Services;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GRINTSYS.SAPMiddleware.Authorization;
using GRINTSYS.SAPMiddleware.M2;
using GRINTSYS.SAPMiddleware.Sizes.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Sizes
{
    [AbpAuthorize(PermissionNames.Pages_MobileAccess)]
    public class SizeAppService : AsyncCrudAppService<Size, SizeDto, int, GetAllSizeInput>, ISizeAppService
    {
        public SizeAppService(IRepository<Size> respository)
           : base(respository)
        {
            CreatePermissionName = PermissionNames.Pages_MobileAccess;
        }

        public override async Task<SizeDto> Create(SizeDto input)
        {
            CheckCreatePermission();

            var obj = ObjectMapper.Map<Size>(input);

            //CheckErrors(await _roleManager.CreateAsync(role));

            await this.Repository.InsertAsync(obj);

            return MapToEntityDto(obj);
        }

        protected override IQueryable<Size> CreateFilteredQuery(GetAllSizeInput input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(input.TenantId.HasValue, t => t.TenantId == input.TenantId.Value)
                .WhereIf(!String.IsNullOrEmpty(input.Value), t => t.Value.Contains(input.Value));
        }
    }
}
