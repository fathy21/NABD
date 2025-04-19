using System.ComponentModel.DataAnnotations;

namespace NABD.Models.Domain
{
    public class MQTTMessage
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string Message { get; set; }
        [Range(0, 100)]
        public int? OxygenSaturation { get; set; } = 98;
        [Range(30, 45)]
        public float? BodyTemperature { get; set; } = 37.0f;
        [Range(40, 180)]
        public int? HeartRate { get; set; } = 70;
        public DateTime VitalDataTimestamp { get; set; } = DateTime.UtcNow;


        public int ToolId { get; set; }
        public Tool Tool { get; set; }
    }
}
