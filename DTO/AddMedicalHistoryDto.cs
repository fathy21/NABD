using System.ComponentModel.DataAnnotations;

namespace NABD.DTO
{
    public class AddMedicalHistoryDto
    {
        [Required]
        [MaxLength(150)]
        public string Diagnosis { get; set; }   

        [Required]
        [MaxLength(150)]
        public string Medication { get; set; }  

        public int PatientId { get; set; }
    }
}
