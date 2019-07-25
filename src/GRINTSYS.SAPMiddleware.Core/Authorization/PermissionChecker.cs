using Abp.Authorization;
using GRINTSYS.SAPMiddleware.Authorization.Roles;
using GRINTSYS.SAPMiddleware.Authorization.Users;

namespace GRINTSYS.SAPMiddleware.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
