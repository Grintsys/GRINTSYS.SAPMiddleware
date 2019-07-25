using System.Collections.Generic;
using GRINTSYS.SAPMiddleware.Roles.Dto;

namespace GRINTSYS.SAPMiddleware.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}