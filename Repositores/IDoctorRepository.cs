using NABD.Models.Domain;

namespace NABD.Repositores
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetAllDoctorsAsync();
        Task<Doctor> GetDoctorByIdAsync(int doctorId);
        Task<Doctor> GetDoctorByNameAsync(string name);
        Task<IEnumerable<Nurse>> GetAllNursesForDoctorAsync(int doctorId);
        Task<Doctor> AddDoctorAsync(Doctor doctor);
        void UpdateDoctor(Doctor doctor);
        Task DeleteDoctorAsync(int doctorId);
        Task<bool> SaveChangesAsync();
    }
}
