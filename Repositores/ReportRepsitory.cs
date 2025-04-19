using Microsoft.EntityFrameworkCore;
using NABD.Data;
using NABD.Models.Domain;

namespace NABD.Repositores
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ReportRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Report> Create(Report report)
        {
            await dbContext.AddAsync(report);
            await dbContext.SaveChangesAsync();
            return report;
        }
        public async Task<Report> Delete(int id)
        {
            var exestingReport = await dbContext.Reports.FirstOrDefaultAsync(x => x.Id == id);
            if (exestingReport == null)
            {
                return null;
            }
            dbContext.Reports.Remove(exestingReport);
            await dbContext.SaveChangesAsync();
            return exestingReport;
        }
        public async Task<List<Report>> GetAll(int pageNumber = 1, int pageSize = 1000)
        {
            var skipresults = (pageNumber - 1) * pageSize;
            return await dbContext.Reports.Skip(skipresults).Take(pageSize).ToListAsync();
        }
        public async Task<Report> GetById(int id)
        {
            return await dbContext.Reports.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Report> Update(int id, Report report)
        {
            var exestingReport = await dbContext.Reports.FirstOrDefaultAsync(x => x.Id == id);
            if (exestingReport == null)
            {
                return null;
            }

            exestingReport.UploadDate = report.UploadDate;
            exestingReport.ReportDetails = report.ReportDetails;

            await dbContext.SaveChangesAsync();
            return exestingReport;
        }
    }
}
