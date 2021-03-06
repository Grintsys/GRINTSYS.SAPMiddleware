﻿using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace GRINTSYS.SAPMiddleware.Authorization
{
    public class SAPMiddlewareAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_MobileAccess, L("MobileAccess"));
            context.CreatePermission(PermissionNames.Pages_M2Admin, L("M2Admin"));
            context.CreatePermission(PermissionNames.Pages_HangfireAccess, L("HangfireAccess"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, SAPMiddlewareConsts.LocalizationSourceName);
        }
    }
}
