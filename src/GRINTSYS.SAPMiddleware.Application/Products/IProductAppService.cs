using Abp.Application.Services;
using Abp.Application.Services.Dto;
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
        PagedResultDto<ProductOutput> GetAllProducts(GetAllProductInput input);
        PagedResultDto<ProductOutput> Search(SearchProductInput input);
        //double CalculateSubtotal(int cartId);
    }
}
