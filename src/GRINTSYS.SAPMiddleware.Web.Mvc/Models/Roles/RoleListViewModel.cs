using System.Collections.Generic;
using GRINTSYS.SAPMiddleware.Roles.Dto;

namespace GRINTSYS.SAPMiddleware.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<RoleListDto> Roles { get; set; }

        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
