using NABD.Models.Domain;

namespace NABD.DTO
{
    public class GetAllNotificationsForGuardianDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }
        public DateTime Date { get; set; }
        public NotificationStatus Status { get; set; }
    }
}
