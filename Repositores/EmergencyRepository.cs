using Microsoft.EntityFrameworkCore;
using NABD.Data;
using NABD.Models.Domain;

namespace NABD.Repositores
{
    public class EmergencyRepository : IEmergencyRepository
    {
        private readonly ApplicationDbContext dbContext;

        public EmergencyRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Emergency> Delete(int id)
        {
            var exestingEmegency = await dbContext.Emergencies.FirstOrDefaultAsync(x => x.Id == id);
            if (exestingEmegency == null)
            {
                return null;
            }

            dbContext.Emergencies.Remove(exestingEmegency);
            await dbContext.SaveChangesAsync();
            return exestingEmegency;
        }
        public async Task<List<Emergency>> GetAll(int pageNumber = 1, int pageSize = 1000)
        {
            var skipresult = (pageNumber - 1) * pageSize;
            return await dbContext.Emergencies.Skip(skipresult).Take(pageSize).ToListAsync();
        }
        public async Task<Emergency> GetById(int Id)
        {
            return await dbContext.Emergencies.FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}
