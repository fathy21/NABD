using System.ComponentModel.DataAnnotations;

namespace NABD.DTO
{
    public class AddGuardianDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Relationship { get; set; }    
        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        public int PatientId { get; set; }
    }
}
