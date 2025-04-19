using NABD.Models.Domain;
using NABD.DTO;

namespace NABD.Repositores
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAll(int pageNumber = 1, int pageSize = 1000);
        Task<Patient?> GetById(int id);
        Task<Patient> GetByName(string name);
        Task<Patient> Create(Patient patient);
        Task<Patient> Delete(int id);
        Task<List<GetAllDoctorsForPatientDto>> GetDoctorsForPatient(int id);
        Task<List<GetAllGuardiansDto>> GetAllGuardiansForPatient(int id);
        Task<GetMedicalHistoryDto> GetMedicalHistoryForPatient(int id);
        Task<List<GetAllReportsDto>> GetReportsForPatient(int id);
        Task<PatientDetailsDto> GetPatientDetails(int id);
    }
}
