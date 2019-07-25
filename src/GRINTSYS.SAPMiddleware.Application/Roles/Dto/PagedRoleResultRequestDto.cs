using Abp.Application.Services.Dto;

namespace GRINTSYS.SAPMiddleware.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

