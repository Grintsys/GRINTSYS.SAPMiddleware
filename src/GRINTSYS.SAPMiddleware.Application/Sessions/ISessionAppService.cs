using System.Threading.Tasks;
using Abp.Application.Services;
using GRINTSYS.SAPMiddleware.Sessions.Dto;

namespace GRINTSYS.SAPMiddleware.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
