using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NABD.Models.Domain
{
    public class Emergency
    {
        public int Id { get; set; }
        [Required, MaxLength(500)]
        public string EmergencyDetails { get; set; }
        public DateTime EmergencyDate { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(Patient))]
        public int? PatientId { get; set; }
        public Patient Patient { get; set; } //M:1

        [ForeignKey(nameof(Tool))]
        public int? ToolId { get; set; }
        public Tool Tool { get; set; } //M:1

        [ForeignKey(nameof(Doctor))]
        public int? DoctorId { get; set; }
        public Doctor Doctor { get; set; } //M:1

        [ForeignKey(nameof(Nurse))]
        public int? NurseId { get; set; }
        public Nurse Nurse { get; set; } //M:1

        [ForeignKey(nameof(Guardian))]
        public int? GuardianId { get; set; }
        public Guardian Guardian { get; set; } //M:1
    }
}
