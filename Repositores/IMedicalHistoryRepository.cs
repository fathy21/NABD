using NABD.Models.Domain;

namespace NABD.Repositores
{
    public interface IMedicalHistoryRepository
    {
        Task<List<MedicalHistory>> GetAll(int pageNumber = 1, int pageSize = 1000);
        Task<MedicalHistory> Create(MedicalHistory medicalHistory);
        Task<MedicalHistory> Update(int id, MedicalHistory medicalHistory);
        Task<MedicalHistory?> GetById(int id);
        Task<MedicalHistory?> Delete(int id);
    }
}
