using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NABD.Models.Domain
{
    public class Patient
    {
        public int Id { get; set; }
        [Required, StringLength(20)]
        public string SSN { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Gender { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [Required,Phone]
        public string PhoneNumber { get; set; }
        public byte[]? PersonalImage { get; set; }


        public required Tool Tool { get; set; } //1:1
        public required MedicalHistory MedicalHistory { get; set; } //1:1
        public ICollection<PatientDoctor> PatientDoctors { get; set; } = new HashSet<PatientDoctor>(); //M:M
        public ICollection<PatientGuardian> PatientGuardians { get; set; } = new HashSet<PatientGuardian>(); //M:M
        public ICollection<Report> Reports { get; set; } = new HashSet<Report>(); //1:M
        public ICollection<Emergency> Emergencies { get; set; } = new HashSet<Emergency>(); //1:M
        public ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>(); //1:M
    }
}
