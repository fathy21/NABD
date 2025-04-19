using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NABD.Data;
using NABD.Models.Domain;

namespace NABD.Repositores
{
    public class MedicalHistoryRepository : IMedicalHistoryRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public MedicalHistoryRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<MedicalHistory> Create(MedicalHistory medicalHistory)
        {
            var medicalhistory = await dbContext.MedicalHistory.AddAsync(medicalHistory);
            await dbContext.SaveChangesAsync();
            return medicalHistory;
        }
        public async Task<MedicalHistory?> Delete(int id)
        {
            var medicalhistory = await dbContext.MedicalHistory.FirstOrDefaultAsync(x => x.Id == id);

            if (medicalhistory == null)
            {
                return null;
            }

            dbContext.MedicalHistory.Remove(medicalhistory);
            await dbContext.SaveChangesAsync();
            return medicalhistory;
        }
        public async Task<List<MedicalHistory>> GetAll(int pageNumber = 1, int pageSize = 1000)
        {
            return await dbContext.MedicalHistory.ToListAsync();
        }
        public async Task<MedicalHistory?> GetById(int id)
        {
            return await dbContext.MedicalHistory.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<MedicalHistory> Update(int id, MedicalHistory medicalHistory)
        {
            var exestingMediaclHistory = await dbContext.MedicalHistory.FirstOrDefaultAsync(x => x.Id == id);

            if (exestingMediaclHistory == null)
            {
                return null;
            }

            exestingMediaclHistory.Medication = medicalHistory.Medication;
            exestingMediaclHistory.Diagnosis = medicalHistory.Diagnosis;

            await dbContext.SaveChangesAsync();

            return exestingMediaclHistory;
        }
    }
}
