using NABD.Models.Domain;

namespace NABD.Repositores
{
    public interface IToolRepository
    {
        Task<IEnumerable<Tool>> GetAllAsync();
        Task<Tool?> GetByIdAsync(int id);
        Task<Tool> CreateAsync(Tool tool);
        Task<Tool> UpdateAsync(int Id,Tool tool);
        Task<bool> DeleteAsync(int id);
    }
}
