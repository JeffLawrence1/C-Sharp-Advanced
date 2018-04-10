using System.ComponentModel.DataAnnotations;

namespace formSubmission_project.Models
{
    public class User : BaseEntity
    {
        [Required]
        [MinLength(4)]
        public string firstName { get; set; }

        [Required]
        [MinLength(4)]
        public string lastName { get; set; }

        [Required]
        
        [Range(1, 200)]
        public int age { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}