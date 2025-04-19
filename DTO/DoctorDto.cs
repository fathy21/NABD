using NABD.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace NABD.DTO
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string SSN { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Specialization { get; set; }

        public ICollection<PatientDoctor> PatientDoctors { get; set; } = new HashSet<PatientDoctor>(); 
        public ICollection<Nurse> Nurses { get; set; } = new HashSet<Nurse>(); 
    }
}
