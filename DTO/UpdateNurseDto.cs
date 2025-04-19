using NABD.Models.Domain;

namespace NABD.DTO
{
    public class UpdateNurseDto
    {
        public string Name { get; set; }
        public string SSN { get; set; }
        public string Role { get; set; }
        public string Ward { get; set; }
        public ShiftType Shift { get; set; }
    }
}
