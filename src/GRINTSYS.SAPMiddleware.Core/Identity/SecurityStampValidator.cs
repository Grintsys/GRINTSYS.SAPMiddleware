using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using GRINTSYS.SAPMiddleware.Authorization.Roles;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using GRINTSYS.SAPMiddleware.MultiTenancy;

namespace GRINTSYS.SAPMiddleware.Identity
{
    public class SecurityStampValidator : AbpSecurityStampValidator<Tenant, Role, User>
    {
        public SecurityStampValidator(
            IOptions<SecurityStampValidatorOptions> options, 
            SignInManager signInManager,
            ISystemClock systemClock) 
            : base(
                  options, 
                  signInManager, 
                  systemClock)
        {
        }
    }
}
