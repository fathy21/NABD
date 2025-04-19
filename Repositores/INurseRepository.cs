using NABD.Models.Domain;

namespace NABD.Repositores
{
    public interface INurseRepository
    {
        Task<List<Nurse>> GetAllNursesAsync();
        Task<Nurse> GetNurseByIdAsync(int nurseId);
        Task AddNurseAsync(Nurse nurse);
        void UpdateNurse(Nurse nurse);
        Task DeleteNurseAsync(int nurseId);
        Task<bool> SaveChangesAsync();
    }
}
