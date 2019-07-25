using Abp.Application.Services;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GRINTSYS.SAPMiddleware.Authorization;
using GRINTSYS.SAPMiddleware.Banks.Dto;
using GRINTSYS.SAPMiddleware.M2;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Banks
{
    [AbpAuthorize(PermissionNames.Pages_MobileAccess)]
    public class BankAppService : AsyncCrudAppService<Bank, BankDto, int, GetAllBankInput>, IBankAppService
    {
        public BankAppService(IRepository<Bank> respository)
           : base(respository)
        {
            CreatePermissionName = PermissionNames.Pages_MobileAccess;
        }

        public override async Task<BankDto> Create(BankDto input)
        {
            CheckCreatePermission();

            var obj = ObjectMapper.Map<Bank>(input);

            //CheckErrors(await _roleManager.CreateAsync(role));

            await this.Repository.InsertAsync(obj);

            return MapToEntityDto(obj);
        }

        protected override IQueryable<Bank> CreateFilteredQuery(GetAllBankInput input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(input.TenantId.HasValue, t => t.TenantId == input.TenantId.Value)
                .WhereIf(!String.IsNullOrEmpty(input.Name), t => t.Name.Contains(input.Name))
                ;
        }
    }
}
