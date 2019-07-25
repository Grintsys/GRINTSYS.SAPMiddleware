using System.ComponentModel.DataAnnotations;

namespace GRINTSYS.SAPMiddleware.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}