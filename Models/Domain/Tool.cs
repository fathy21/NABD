using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NABD.Models.Domain
{
    public class Tool
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string QrCode { get; set; }

        //TODO: Add a property to store the tool's status
        //TODO: Make PatientId Not required

        [ForeignKey(nameof(Patient))]
        public int? PatientId { get; set; }
        public Patient? Patient { get; set; } //1:1
        public ICollection<Emergency> Emergencies { get; set; } = new HashSet<Emergency>(); //1:M
        public ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>(); //1:M
        public ICollection<MQTTMessage> MQTTMessages { get; set; } = new HashSet<MQTTMessage>(); //1:M
    }
}
