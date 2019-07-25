using Abp.Application.Services;
using GRINTSYS.SAPMiddleware.Brands.Dto;

namespace GRINTSYS.SAPMiddleware.Brands
{
    public interface IBrandAppService : IAsyncCrudAppService<BrandDto, int, GetAllBrandInput>
    {
    }
}
