using Abp.Application.Navigation;
using Abp.Localization;
using GRINTSYS.SAPMiddleware.Authorization;

namespace GRINTSYS.SAPMiddleware.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class SAPMiddlewareNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Home,
                        L("HomePage"),
                        url: "",
                        icon: "home",
                        requiresAuthentication: true
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Tenants,
                        L("Tenants"),
                        url: "Tenants",
                        icon: "business",
                        requiredPermissionName: PermissionNames.Pages_Tenants
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Users,
                        L("Users"),
                        url: "Users",
                        icon: "people",
                        requiredPermissionName: PermissionNames.Pages_Users
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Roles,
                        L("Roles"),
                        url: "Roles",
                        icon: "local_offer",
                        requiredPermissionName: PermissionNames.Pages_Roles
                    )
                ).AddItem( // Menu items below is just for demonstration!
                    new MenuItemDefinition(
                        "M2Module",
                        L("M2Module"),
                        icon: "menu"
                    ).AddItem(
                        new MenuItemDefinition(
                            "Order",
                             L("Order"),
                             url: "Order",
                             icon: "info",
                             requiredPermissionName: PermissionNames.Pages_M2Admin
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            "Payment",
                             L("Payment"),
                             url: "Payment",
                             icon: "info",
                             requiredPermissionName: PermissionNames.Pages_M2Admin
                      )
                    ).AddItem(
                        new MenuItemDefinition(
                            "SapInvoice",
                             L("SapInvoice"),
                             url: "SapInvoice",
                             icon: "info",
                             requiredPermissionName: PermissionNames.Pages_M2Admin_SapInvoice
                      )
                    )
            );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, SAPMiddlewareConsts.LocalizationSourceName);
        }
    }
}
