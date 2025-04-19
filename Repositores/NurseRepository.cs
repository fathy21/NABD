using Microsoft.EntityFrameworkCore;
using NABD.Data;
using NABD.Models.Domain;

namespace NABD.Repositores
{
    public class NurseRepository : INurseRepository
    {
        private readonly ApplicationDbContext _context;

        public NurseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Nurse>> GetAllNursesAsync()
        {
            return await _context.Nurses.ToListAsync();
        }

        public async Task<Nurse> GetNurseByIdAsync(int nurseId)
        {
            return await _context.Nurses.FindAsync(nurseId);
        }

        public async Task AddNurseAsync(Nurse nurse)
        {
            await _context.Nurses.AddAsync(nurse);
        }

        public void UpdateNurse(Nurse nurse)
        {
            _context.Nurses.Update(nurse);
        }

        public async Task DeleteNurseAsync(int nurseId)
        {
            var nurse = await _context.Nurses.FindAsync(nurseId);
            if (nurse != null)
            {
                _context.Nurses.Remove(nurse);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
