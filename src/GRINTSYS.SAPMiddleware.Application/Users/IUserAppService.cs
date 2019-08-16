using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GRINTSYS.SAPMiddleware.Roles.Dto;
using GRINTSYS.SAPMiddleware.Users.Dto;

namespace GRINTSYS.SAPMiddleware.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);

        UserDto GetUserDetails(long id);
    }
}
