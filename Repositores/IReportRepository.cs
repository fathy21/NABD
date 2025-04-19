using NABD.Models.Domain;

namespace NABD.Repositores
{
    public interface IReportRepository
    {
        Task<List<Report>> GetAll(int pageNumber = 1, int pageSize = 1000);
        Task<Report> GetById(int id);
        Task<Report> Create(Report report);
        Task<Report> Update(int id, Report report);
        Task<Report> Delete(int id);
    }
}
