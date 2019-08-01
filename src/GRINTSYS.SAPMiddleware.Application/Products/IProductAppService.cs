using Abp.Application.Services;
using GRINTSYS.SAPMiddleware.Products.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Products
{
    public interface IProductAppService : IApplicationService
    {
        Task CreateProduct(AddProductInput input);
        Task CreateProductVariant(AddProductVariantInput input);

        ProductDto GetProduct(GetProductInput input);
        //List<ProductVariantDto> GetProductVariants(GetProductVariantInput input);
    }
}
