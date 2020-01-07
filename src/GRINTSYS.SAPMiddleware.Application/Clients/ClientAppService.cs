using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GRINTSYS.SAPMiddleware.Authorization;
using GRINTSYS.SAPMiddleware.Clients.Dto;
using GRINTSYS.SAPMiddleware.M2;
using GRINTSYS.SAPMiddleware.M2.Clients;
using GRINTSYS.SAPMiddleware.M2.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace GRINTSYS.SAPMiddleware.Clients
{
    [AbpAuthorize(PermissionNames.Pages_MobileAccess)]
    public class ClientAppService : AsyncCrudAppService<Client, ClientDto, int, GetAllClientInput>, IClientAppService
    {
        private readonly ClientManager _clientManager;
        public ClientAppService(IRepository<Client> respository, ClientManager clientManager)
           : base(respository)
        {
            CreatePermissionName = PermissionNames.Pages_MobileAccess;
            _clientManager = clientManager;
        }

        public override async Task<ClientDto> Create(ClientDto input)
        {
            CheckCreatePermission();

            var obj = ObjectMapper.Map<Client>(input);

            //CheckErrors(await _roleManager.CreateAsync(role));

            await this.Repository.InsertAsync(obj);

            return MapToEntityDto(obj);
        }

        public ClientDto GetClient(GetClientInput input)
        {
            var client = _clientManager.GetClient(input.Id);

            var obj = ObjectMapper.Map<ClientDto>(client);

            return obj;
        }
         

        public PagedResultDto<ClientDto> GetClientBySearchQuery(ClientSearchInput input)
        {
            if (input.MaxResultCount <= 0)
                input.MaxResultCount = AppConsts.MaxResultCount;

            if (String.IsNullOrEmpty(input.Sorting))
                input.Sorting = AppConsts.DefaultSortingField;

            var clients =  this.Repository.GetAll()
                .WhereIf(!String.IsNullOrEmpty(input.SearchText), t => t.Name.Contains(input.SearchText) || t.CardCode.Contains(input.SearchText) || t.ContactPerson.Contains(input.SearchText))
                .WhereIf(input.TenantId.HasValue, w => w.TenantId == input.TenantId)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToList();

            var itemsCount = clients.Count();

            return new PagedResultDto<ClientDto>
            {
                TotalCount = itemsCount,
                Items = clients.MapTo<List<ClientDto>>()
            };
        }

        public double GetClientDiscount(string cardcode, int itemgroup)
        {
            return _clientManager.GetClientDiscountByItemGroupCode(cardcode, itemgroup);
        }

        public ClientDto GetClientDocuments(GetClientInput input)
        {
            var client = _clientManager.GetClient(input.Id);

            var obj = ObjectMapper.Map<ClientDto>(client);

            return obj;
        }

        public ClientDto GetClientDocumentsByCardCode(GetClientInput input)
        {
            var client = _clientManager.GetClient(input.CardCode);

            var obj = ObjectMapper.Map<ClientDto>(client);

            return obj;
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
