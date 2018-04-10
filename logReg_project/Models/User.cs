using System.ComponentModel.DataAnnotations;

namespace logReg_project.Models
{
    public class User : BaseEntity
    {
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MinLength(8)]
        [Compare("Password")]
        public string Confirm { get; set; }
    }
}