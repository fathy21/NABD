using System.ComponentModel.DataAnnotations;

namespace NABD.DTO
{
    public class CreateToolDto
    {
        [Required, StringLength(100)]
        public string QrCode { get; set; }
        public int? PatientId { get; set; }
    }
}
