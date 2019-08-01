using Abp.Application.Services;
using Abp.Dependency;
using GRINTSYS.SAPMiddleware.Products.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Products
{
    public interface IProductAppService : IApplicationService, ITransientDependency
    {
        Task CreateProduct(AddProductInput input);
        Task CreateVariant(AddProductVariantInput input);
        ProductOutput GetProduct(GetProductInput input);
        List<ProductOutput> GetAllProducts(GetAllProductInput input);
    }
}
