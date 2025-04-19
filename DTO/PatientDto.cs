using NABD.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace NABD.DTO
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string SSN { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }


        [ForeignKey(nameof(MedicalHistory))]
        public int? MedicalHistoryId { get; set; }
        public MedicalHistory MedicalHistory { get; set; }
        public ICollection<PatientDoctor> Doctors { get; set; } = new HashSet<PatientDoctor>();
        public ICollection<PatientGuardian> Gurdians { get; set; } = new HashSet<PatientGuardian>();
    }   
}
