using Abp.Application.Services;
using GRINTSYS.SAPMiddleware.Categories.Dto;

namespace GRINTSYS.SAPMiddleware.Categories
{
    public interface ICategoryAppService : IAsyncCrudAppService<CategoryDto, int, GetAllCategoryInput>
    {
    }
}
