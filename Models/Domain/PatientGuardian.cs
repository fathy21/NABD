namespace NABD.Models.Domain
{
    public class PatientGuardian
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int GuardianId { get; set; }
        public Guardian Guardian { get; set; }
    }
}
