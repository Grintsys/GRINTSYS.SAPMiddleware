using System.ComponentModel.DataAnnotations;

namespace GRINTSYS.SAPMiddleware.Configuration.Dto
{
    public class ChangeUiThemeInput
    {
        [Required]
        [StringLength(32)]
        public string Theme { get; set; }
    }
}
