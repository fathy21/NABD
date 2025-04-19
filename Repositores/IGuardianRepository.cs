using NABD.DTO;
using NABD.Models.Domain;

namespace NABD.Repositores
{
    public interface IGuardianRepository
    {
        Task<List<Guardian>> GetAll(int pageNumber = 1, int pageSize = 1000);
        Task<Guardian> Create(Guardian gurdian,int patientId);
        Task<Guardian?> GetById(int Id);
        Task<Guardian?> GetByName(string Name);
        Task<Guardian> Update(int Id, Guardian gurdian);
        Task<Guardian> Delete(int Id);
        Task<GetPatientByGuardianIdDto> getPatientByGuardianID(int GuardianId);
        Task<List<GetAllNotificationsForGuardianDto>> getAllNotificationsforGuardians(int GuardianId);
        Task<List<GetAllEmergenciesForGuardianDto>> getAllEmergenciesforGuardians(int GuardianId);
    }
}
