using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using GRINTSYS.SAPMiddleware.Cart;
using GRINTSYS.SAPMiddleware.Carts.Dto;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Carts
{
    //[AbpAuthorize(PermissionNames.Pages_MobileUsers)]
    public class CartAppService: AsyncCrudAppService<M2.Cart, CartDto, int, GetAllUserCartInput>, ICartAppService
    {
        private readonly IRepository<M2.Cart> _cartRepository;

        public CartAppService(IRepository<M2.Cart> cartRespository)
            :base(cartRespository)
        {
            _cartRepository = cartRespository;
            //CreatePermissionName = PermissionNames.Pages_MobileAccess;
            //DeletePermissionName = PermissionNames.Pages_MobileAccess;
        }
      
        public override async Task<CartDto> Create(CartDto input)
        {
            Logger.Info("Creating a cart for input: " + input);

            CheckCreatePermission();

            // I can't use automapper because foreign key confict maybe there are a good solution
            // but i dont have time so the mapping is doing manually
            var cart = new M2.Cart()
            {
                UserId = input.UserId,
                Currency = input.Currency,
                //Type = input.Type
            };

            await _cartRepository.InsertAsync(cart);

            return MapToEntityDto(cart);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public override async Task Delete(EntityDto<int> input)
        {
            Logger.Info("Deleting a cart for input: " + input);

            CheckDeletePermission();

            var cart = await _cartRepository.FirstOrDefaultAsync(input.Id);
            await _cartRepository.DeleteAsync(cart);
        }
        
        protected override IQueryable<M2.Cart> CreateFilteredQuery(GetAllUserCartInput input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(input.TenantId.HasValue, t => t.TenantId == input.TenantId.Value)
                .WhereIf(input.Type.HasValue, t => t.Type == (int)input.Type.Value);
        }
    }
}
