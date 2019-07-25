using Abp.Application.Services;
using GRINTSYS.SAPMiddleware.Banks.Dto;

namespace GRINTSYS.SAPMiddleware.Banks
{
    public interface IBankAppService : IAsyncCrudAppService<BankDto, int, GetAllBankInput>
    {
    }
}
