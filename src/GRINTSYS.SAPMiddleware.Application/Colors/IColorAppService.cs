using Abp.Application.Services;
using GRINTSYS.SAPMiddleware.Colors.Dto;

namespace GRINTSYS.SAPMiddleware.Colors
{
    public interface IColorAppService : IAsyncCrudAppService<ColorDto, int, GetAllColorInput>
    {
    }
}
