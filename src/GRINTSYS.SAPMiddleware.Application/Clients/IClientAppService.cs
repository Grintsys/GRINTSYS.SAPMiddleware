﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GRINTSYS.SAPMiddleware.Clients.Dto;
using System.Collections.Generic;

namespace GRINTSYS.SAPMiddleware.Clients
{
    public interface IClientAppService : IAsyncCrudAppService<ClientDto, int, GetAllClientInput>
    {
        PagedResultDto<ClientDto> GetClientBySearchQuery(ClientSearchInput input);
    }
}
