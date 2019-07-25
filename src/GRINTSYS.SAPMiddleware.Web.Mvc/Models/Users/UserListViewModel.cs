using System.Collections.Generic;
using GRINTSYS.SAPMiddleware.Roles.Dto;
using GRINTSYS.SAPMiddleware.Users.Dto;

namespace GRINTSYS.SAPMiddleware.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
