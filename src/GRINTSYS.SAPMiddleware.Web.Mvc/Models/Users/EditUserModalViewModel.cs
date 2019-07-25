using System.Collections.Generic;
using System.Linq;
using GRINTSYS.SAPMiddleware.Roles.Dto;
using GRINTSYS.SAPMiddleware.Users.Dto;

namespace GRINTSYS.SAPMiddleware.Web.Models.Users
{
    public class EditUserModalViewModel
    {
        public UserDto User { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }

        public bool UserIsInRole(RoleDto role)
        {
            return User.RoleNames != null && User.RoleNames.Any(r => r == role.NormalizedName);
        }
    }
}
