using Abp.Application.Services;
using GRINTSYS.SAPMiddleware.Banners.Dto;

namespace GRINTSYS.SAPMiddleware.Banners
{
    public interface IBannerAppService : IAsyncCrudAppService<BannerDto, int, GetAllBannerInput>
    {
    }
}
