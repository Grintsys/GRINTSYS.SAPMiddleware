using Abp.Application.Services.Dto;
using System;

namespace GRINTSYS.SAPMiddleware.Users.Dto
{
    //custom PagedResultRequestDto
    public class PagedUserResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}
