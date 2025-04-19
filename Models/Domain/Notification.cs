using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NABD.Models.Domain
{
    public enum NotificationStatus
    {
        Unread,
        Read,
        Dismissed
    }

    public enum NotificationType
    {
        EmergencyAlert,
        NewReport,
        SystemUpdate,
        ToolIssue
    }

    public class Notification
    {
        public int Id { get; set; }
        [Required, MaxLength(500)]
        public string Message { get; set; }
        public NotificationType Type { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public NotificationStatus Status { get; set; } = NotificationStatus.Unread;

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
