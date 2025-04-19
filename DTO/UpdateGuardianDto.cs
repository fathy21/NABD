using System.ComponentModel.DataAnnotations;

namespace NABD.DTO
{
    public class UpdateGuardianDto
    {
        [Required]
        [MaxLength(50)]
        public string Relationship { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(70)]
        public string PhoneNumber { get; set; }
    }
}
