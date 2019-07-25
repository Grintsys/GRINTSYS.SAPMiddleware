using Abp.Application.Services;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GRINTSYS.SAPMiddleware.Authorization;
using GRINTSYS.SAPMiddleware.Categories.Dto;
using GRINTSYS.SAPMiddleware.M2;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Categories
{
    [AbpAuthorize(PermissionNames.Pages_MobileAccess)]
    public class CategoryAppService : AsyncCrudAppService<Category, CategoryDto, int, GetAllCategoryInput>, ICategoryAppService
    {
        public CategoryAppService(IRepository<Category> categoryRespository)
           : base(categoryRespository)
        {
            CreatePermissionName = PermissionNames.Pages_MobileAccess;
        }

        public override async Task<CategoryDto> Create(CategoryDto input)
        {
            CheckCreatePermission();

            var category = ObjectMapper.Map<Category>(input);

            //CheckErrors(await _roleManager.CreateAsync(role));

            await this.Repository.InsertAsync(category);

            return MapToEntityDto(category);
        }

        protected override IQueryable<Category> CreateFilteredQuery(GetAllCategoryInput input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(input.TenantId.HasValue, t => t.TenantId == input.TenantId.Value)
                .WhereIf(input.PartentId.HasValue, t => t.PartentId == input.PartentId.Value)
                .WhereIf(!String.IsNullOrEmpty(input.Name), t => t.Name.Contains(input.Name))
                .WhereIf(!String.IsNullOrEmpty(input.Type), t => t.Type.Contains(input.Type));
            ;
        }
    }
}
