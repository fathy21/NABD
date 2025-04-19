using System.ComponentModel.DataAnnotations;

namespace NABD.DTO
{
    public class AddPatientDto
    {
        [Required]
        public string SSN { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
