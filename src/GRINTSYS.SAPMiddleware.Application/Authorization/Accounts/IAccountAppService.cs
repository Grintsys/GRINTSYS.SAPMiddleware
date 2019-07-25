using System.Threading.Tasks;
using Abp.Application.Services;
using GRINTSYS.SAPMiddleware.Authorization.Accounts.Dto;

namespace GRINTSYS.SAPMiddleware.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
