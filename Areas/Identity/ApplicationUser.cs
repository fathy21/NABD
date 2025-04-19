using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace NABD.Areas.Identity
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string NationalId { get; set; } = string.Empty;

        [Required]
        public string UserType { get; set; } = string.Empty;

        public string? Specialty { get; set; }
        public new string? PhoneNumber { get; set; }
    }
}
