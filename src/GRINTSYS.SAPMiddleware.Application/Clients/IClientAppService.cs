using Abp.Application.Services;
using GRINTSYS.SAPMiddleware.Clients.Dto;
using System.Collections.Generic;

namespace GRINTSYS.SAPMiddleware.Clients
{
    public interface IClientAppService : IAsyncCrudAppService<ClientDto, int, GetAllClientInput>
    {
        List<ClientDto> GetClientBySearchQuery(ClientSearchInput input);
    }
}
