using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NABD.Models.Domain
{
    public class MedicalHistory
    {
        public int Id { get; set; }
        [Required, MaxLength(500)]
        public string Diagnosis { get; set; }
        [MaxLength(500)]
        public string Medication { get; set; }
        public DateTime RecordDate { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(Patient))]
        public int PatientId { get; set; }
        public Patient Patient { get; set; } //1:1
    }
}
