using Abp.Application.Services;
using GRINTSYS.SAPMiddleware.Sizes.Dto;

namespace GRINTSYS.SAPMiddleware.Sizes
{
    public interface ISizeAppService : IAsyncCrudAppService<SizeDto, int, GetAllSizeInput>
    {
    }
}
