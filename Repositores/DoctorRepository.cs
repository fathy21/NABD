using Microsoft.EntityFrameworkCore;
using NABD.Data;
using NABD.Models.Domain;

namespace NABD.Repositores
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<Doctor> GetDoctorByIdAsync(int doctorId)
        {
            return await _context.Doctors.FindAsync(doctorId);
        }

        public async Task<Doctor> GetDoctorByNameAsync(string name)
        {
            return await _context.Doctors.FirstOrDefaultAsync(d => d.Name == name);
        }

        public async Task<IEnumerable<Nurse>> GetAllNursesForDoctorAsync(int doctorId)
        {
            return await _context.Nurses
                .Where(n => n.DoctorId == doctorId)  // Assuming there's a foreign key AssignedDoctorId
                .ToListAsync();
        }

        public async Task<Doctor> AddDoctorAsync(Doctor doctor)
        {
            await _context.AddAsync(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }

        public void UpdateDoctor(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
        }

        public async Task DeleteDoctorAsync(int doctorId)
        {
            var doctor = await _context.Doctors.FindAsync(doctorId);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
