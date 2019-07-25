using Abp.Application.Navigation;

namespace GRINTSYS.SAPMiddleware.Web.Views.Shared.Components.SideBarNav
{
    public class SideBarNavViewModel
    {
        public UserMenu MainMenu { get; set; }

        public string ActiveMenuItemName { get; set; }
    }
}
