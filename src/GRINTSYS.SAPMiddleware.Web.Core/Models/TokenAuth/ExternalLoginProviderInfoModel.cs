using Abp.AutoMapper;
using GRINTSYS.SAPMiddleware.Authentication.External;

namespace GRINTSYS.SAPMiddleware.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
