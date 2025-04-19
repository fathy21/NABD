using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NABD.Models.Domain
{
    public enum ShiftType
    {
        Morning,
        Afternoon,
        Night
    }
    public class Nurse
    {
        public int Id { get; set; }
        [Required, StringLength(20)]
        public string SSN { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(50)]
        public string Role { get; set; }
        public byte[]? PersonalImage { get; set; }
        [Required]
        public string Ward { get; set; }
        [Required]
        public ShiftType Shift { get; set; }
        [ForeignKey(nameof(Doctor))]
        public int? DoctorId { get; set; }
        public Doctor Doctor { get; set; } //M:1
        public ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>(); //1:M
        public ICollection<Emergency> Emergencies { get; set; } = new HashSet<Emergency>(); //1:M
    }
}
