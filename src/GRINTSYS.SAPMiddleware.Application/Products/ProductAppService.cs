using Abp.Application.Services;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GRINTSYS.SAPMiddleware.Authorization;
using GRINTSYS.SAPMiddleware.M2;
using GRINTSYS.SAPMiddleware.M2.Products;
using GRINTSYS.SAPMiddleware.Products.Dto;
using System.Linq;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Products
{
    [AbpAuthorize(PermissionNames.Pages_MobileAccess)]
    public class ProductAppService : AsyncCrudAppService<Product, ProductDto, int, GetAllProductInput, AddProductInput>, IProductAppService
    {

        public ProductAppService(IRepository<Product, int> repository) : base(repository)
        {
        }

        public override async Task<ProductDto> Create(AddProductInput input)
        {
            CheckCreatePermission();

            var product = ObjectMapper.Map<Product>(input);

            //CheckErrors(await _roleManager.CreateAsync(role));

            //await _productManager.CreateProduct(product);

            return MapToEntityDto(product);
        }

        protected override IQueryable<Product> CreateFilteredQuery(GetAllProductInput input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(input.TenantId.HasValue, t => t.TenantId == input.TenantId.Value);
        }

    }
}
