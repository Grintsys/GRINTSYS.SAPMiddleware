using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using GRINTSYS.SAPMiddleware.Authorization.Users;
using GRINTSYS.SAPMiddleware.MultiTenancy;
using GRINTSYS.SAPMiddleware.M2.Products;
using Abp.UI;

namespace GRINTSYS.SAPMiddleware
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class SAPMiddlewareAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected SAPMiddlewareAppServiceBase()
        {
            LocalizationSourceName = SAPMiddlewareConsts.LocalizationSourceName;
        }

        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        public long GetUserId()
        {
            var userId = long.MinValue;
            try
            {
                userId = AbpSession.GetUserId();
            }
            catch (Exception)
            {
                throw new UserFriendlyException("Expired Session");
            }

            return userId;
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
