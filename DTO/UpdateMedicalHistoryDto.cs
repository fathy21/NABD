using System.ComponentModel.DataAnnotations;

namespace NABD.DTO
{
    public class UpdateMedicalHistoryDto
    {
        [Required]
        [MaxLength(100)]
        public string Diagonsis { get; set; }
        [Required]
        [MaxLength(100)]
        public string Medication { get; set; }
    }
}
