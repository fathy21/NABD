namespace NABD.DTO
{
    public class GetPatientByGuardianIdDto
    {
        public int Id { get; set; }
        public string SSN { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
    }
}
