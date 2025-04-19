namespace NABD.DTO
{
    public class PatientDetailsDto
    {
        public int Id { get; set; }
        public string SSN { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }

        public List<GetAllGuardiansDto> Guardians { get; set; }
        public GetMedicalHistoryDto MedicalHistory { get; set; }
        public List<GetAllReportsDto> Reports { get; set; }
    }
}
