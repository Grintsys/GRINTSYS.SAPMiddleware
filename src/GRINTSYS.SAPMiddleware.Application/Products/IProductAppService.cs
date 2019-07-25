using Abp.Application.Services;
using GRINTSYS.SAPMiddleware.Brands.Dto;
using GRINTSYS.SAPMiddleware.Products.Dto;

namespace GRINTSYS.SAPMiddleware.Products
{
    public interface IProductAppService : IAsyncCrudAppService<ProductDto, int, GetAllProductInput>
    {
    }
}
