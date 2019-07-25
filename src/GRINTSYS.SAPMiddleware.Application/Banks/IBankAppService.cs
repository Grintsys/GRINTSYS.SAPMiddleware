using Abp.Application.Services;
using GRINTSYS.SAPMiddleware.Clients.Dto;

namespace GRINTSYS.SAPMiddleware.Clients
{
    public interface IClientAppService : IAsyncCrudAppService<ClientDto, int, GetAllClientInput>
    {
    }
}
