using System.Collections.Generic;

namespace GRINTSYS.SAPMiddleware.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}
