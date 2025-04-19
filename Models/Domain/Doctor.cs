using System.ComponentModel.DataAnnotations;

namespace NABD.Models.Domain
{
    public class Doctor
    {
        public int Id { get; set; }
        [Required, StringLength(20)]
        public string SSN { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [Required, StringLength(50)]
        public string Role { get; set; }
        public byte[]? PersonalImage { get; set; }
        [Required, StringLength(100)]
        public string Specialization { get; set; }

        public ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>(); //1:M
        public ICollection<Report> Reports { get; set; } = new HashSet<Report>(); //1:M
        public ICollection<Emergency> Emergencies { get; set; } = new HashSet<Emergency>(); //1:M
        public ICollection<PatientDoctor> PatientDoctors { get; set; } = new HashSet<PatientDoctor>(); //M:M
        public ICollection<Nurse> Nurses { get; set; } = new HashSet<Nurse>(); //1:M
    }
}
