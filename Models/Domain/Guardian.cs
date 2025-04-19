using System.ComponentModel.DataAnnotations;

namespace NABD.Models.Domain
{
    public class Guardian
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(50)]
        public string Relationship { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [Required, Phone]
        public string PhoneNumber { get; set; }

        public ICollection<PatientGuardian> PatientGuardians { get; set; } = new HashSet<PatientGuardian>(); //M:M
        public ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>(); //1:M
        public ICollection<Emergency> Emergencies { get; set; } = new HashSet<Emergency>(); //1:M  
    }
}
