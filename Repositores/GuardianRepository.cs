using Microsoft.EntityFrameworkCore;
using NABD.Data;
using NABD.DTO;
using NABD.Models.Domain;

namespace NABD.Repositores
{
    public class GuardianRepository : IGuardianRepository
    {
        private readonly ApplicationDbContext dbContext;
        public GuardianRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Guardian>> GetAll(int pageNumber = 1, int pageSize = 1000)
        {
            var Skipresult = (pageNumber - 1) * pageSize;
            return await dbContext.Guardians.Skip(Skipresult).Take(pageSize).ToListAsync();
        }
        public async Task<Guardian> Create(Guardian guardian, int patientId)
        {
            var patient = await dbContext.Patients.FindAsync(patientId);
            if (patient == null)
            {
                throw new Exception("Patient not found!");
            }

            // Add the Guardian first
            await dbContext.Guardians.AddAsync(guardian);
            await dbContext.SaveChangesAsync(); // Save to get GuardianId

            // Create the relation in the join table
            var patientGuardian = new PatientGuardian
            {
                PatientId = patientId,
                GuardianId = guardian.Id
            };

            await dbContext.PatientGuardians.AddAsync(patientGuardian);
            await dbContext.SaveChangesAsync();

            return guardian;
        }
        public async Task<Guardian?> GetById(int Id)
        {
            return await dbContext.Guardians.FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<Guardian?> GetByName(string Name)
        {
            return await dbContext.Guardians.FirstOrDefaultAsync(x => x.Name == Name);
        }
        public async Task<Guardian> Delete(int Id)
        {
            var exestinggurdian = await dbContext.Guardians.FirstOrDefaultAsync(x => x.Id == Id);
            if (exestinggurdian == null)
            {
                return null;
            }
            dbContext.Guardians.Remove(exestinggurdian);
            await dbContext.SaveChangesAsync();
            return exestinggurdian;
        }
        public async Task<GetPatientByGuardianIdDto> getPatientByGuardianID(int GuardianId)
        {
            var guardian = await dbContext.Guardians
             .Include(g => g.PatientGuardians)
             .ThenInclude(pg => pg.Patient)
             .FirstOrDefaultAsync(g => g.Id == GuardianId);

            if (guardian?.PatientGuardians == null)
                return null;
            return guardian.PatientGuardians
                .Select(pg => new GetPatientByGuardianIdDto
                {
                    Id = pg.Patient.Id,
                    SSN = pg.Patient.SSN,
                    Name = pg.Patient.Name,
                    BirthDate = pg.Patient.BirthDate,
                    PhoneNumber = pg.Patient.PhoneNumber
                }).FirstOrDefault();
        }
        public async Task<List<GetAllEmergenciesForGuardianDto>> getAllEmergenciesforGuardians(int GurdianId)
        {
            var gurdian = await dbContext.Guardians
                .Include(x => x.Emergencies)
                .FirstOrDefaultAsync(x => x.Id == GurdianId);

            if (gurdian == null)
            {
                return null;
            }

            return gurdian.Emergencies
                .Select(x => new GetAllEmergenciesForGuardianDto
                {
                    Id = x.Id,
                    EmergencyDetails = x.EmergencyDetails,
                    EmergencyDate = x.EmergencyDate
                }).ToList();
        }
        public async Task<List<GetAllNotificationsForGuardianDto>> getAllNotificationsforGuardians(int GuardianId)
        {
            var guardian = await dbContext.Guardians
                .Include(x => x.Notifications)
                .FirstOrDefaultAsync(x => x.Id == GuardianId);
            if (guardian == null)
            {
                return null;
            }

            return guardian.Notifications
                .Select(x => new GetAllNotificationsForGuardianDto
                {
                    Id = x.Id,
                    Message = x.Message,
                    Type = x.Type,
                    Date = x.Date,
                    Status = x.Status
                }).ToList();
        }
        public async Task<Guardian> Update(int Id, Guardian gurdian)
        {
            var exestinggurdian = await dbContext.Guardians.FirstOrDefaultAsync(x => x.Id == Id);
            if (gurdian == null)
            {
                return null;
            }

            exestinggurdian.PhoneNumber = gurdian.PhoneNumber;
            exestinggurdian.Name = gurdian.Name;
            exestinggurdian.Relationship = gurdian.Relationship;

            await dbContext.SaveChangesAsync();
            return exestinggurdian;
        }     
    }
}
