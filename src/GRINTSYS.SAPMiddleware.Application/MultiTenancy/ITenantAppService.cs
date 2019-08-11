using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GRINTSYS.SAPMiddleware.MultiTenancy.Dto;

namespace GRINTSYS.SAPMiddleware.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
        
    }
}

