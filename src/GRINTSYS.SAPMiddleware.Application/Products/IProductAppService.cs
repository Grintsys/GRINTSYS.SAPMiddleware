using Abp.Application.Services;
using GRINTSYS.SAPMiddleware.Products.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Products
{
    public interface IProductAppService : IAsyncCrudAppService<ProductDto, int, GetAllProductInput, AddProductInput>
    {
    }
}
