using Abp.Application.Services;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GRINTSYS.SAPMiddleware.Authorization;
using GRINTSYS.SAPMiddleware.Colors.Dto;
using GRINTSYS.SAPMiddleware.M2;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Colors
{
    [AbpAuthorize(PermissionNames.Pages_MobileAccess)]
    public class ColorAppService : AsyncCrudAppService<Color, ColorDto, int, GetAllColorInput>, IColorAppService
    {
        public ColorAppService(IRepository<Color> respository)
           : base(respository)
        {
            CreatePermissionName = PermissionNames.Pages_MobileAccess;
        }

        public override async Task<ColorDto> Create(ColorDto input)
        {
            CheckCreatePermission();

            var obj = ObjectMapper.Map<Color>(input);

            //CheckErrors(await _roleManager.CreateAsync(role));

            await this.Repository.InsertAsync(obj);

            return MapToEntityDto(obj);
        }

        protected override IQueryable<Color> CreateFilteredQuery(GetAllColorInput input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(input.TenantId.HasValue, t => t.TenantId == input.TenantId.Value)
                .WhereIf(!String.IsNullOrEmpty(input.Value), t => t.Value.Contains(input.Value))
                .WhereIf(!String.IsNullOrEmpty(input.Code), t => t.Code.Contains(input.Code))
                ;
        }
    }
}
