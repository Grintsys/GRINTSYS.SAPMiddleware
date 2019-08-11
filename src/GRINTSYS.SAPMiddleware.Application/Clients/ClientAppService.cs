using Abp.Application.Services;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GRINTSYS.SAPMiddleware.Authorization;
using GRINTSYS.SAPMiddleware.Clients.Dto;
using GRINTSYS.SAPMiddleware.M2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Clients
{
    [AbpAuthorize(PermissionNames.Pages_MobileAccess)]
    public class ClientAppService : AsyncCrudAppService<Client, ClientDto, int, GetAllClientInput>, IClientAppService
    {
        public ClientAppService(IRepository<Client> respository)
           : base(respository)
        {
            CreatePermissionName = PermissionNames.Pages_MobileAccess;
        }

        public override async Task<ClientDto> Create(ClientDto input)
        {
            CheckCreatePermission();

            var obj = ObjectMapper.Map<Client>(input);

            //CheckErrors(await _roleManager.CreateAsync(role));

            await this.Repository.InsertAsync(obj);

            return MapToEntityDto(obj);
        }

        public List<ClientDto> GetClientBySearchQuery(ClientSearchInput input)
        {
            var clients =  this.Repository.GetAll()
                .WhereIf(!String.IsNullOrEmpty(input.SearchText), t => t.Name.Contains(input.SearchText) || t.CardCode.Contains(input.SearchText) || t.ContactPerson.Contains(input.SearchText))
                .WhereIf(input.TenantId.HasValue, w => w.TenantId == input.TenantId)
                .ToList();

            return clients.MapTo<List<ClientDto>>();
        }

        protected override IQueryable<Client> CreateFilteredQuery(GetAllClientInput input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(input.TenantId.HasValue, t => t.TenantId == input.TenantId.Value)
                .WhereIf(!String.IsNullOrEmpty(input.Name), t => t.Name.Contains(input.Name))
                .WhereIf(!String.IsNullOrEmpty(input.CardCode), t => t.CardCode.Contains(input.CardCode))
                ;
        }
    }
}
