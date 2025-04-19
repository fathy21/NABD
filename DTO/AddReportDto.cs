namespace NABD.DTO
{
    public class AddReportDto
    {
        public DateTime UploadDate { get; set; }
        public string ReportDetails { get; set; }
        public int PatientId { get; set; }
        public int MedicalStaffId { get; set; }
    }
}
