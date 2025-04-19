using NABD.Models.Domain;

namespace NABD.Repositores
{
    public interface IEmergencyRepository
    {
        Task<List<Emergency>> GetAll(int pageNumber = 1, int pageSize = 1000);
        Task<Emergency> GetById(int Id);
        Task<Emergency> Delete(int id);
    }
}
