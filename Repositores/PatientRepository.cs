using Microsoft.EntityFrameworkCore;
using NABD.Data;
using NABD.DTO;
using NABD.Models.Domain;

namespace NABD.Repositores
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Patient> Create(Patient patient)
        {
            await _context.AddAsync(patient);
            await _context.SaveChangesAsync();
            return patient;
        }
        public async Task<Patient> Delete(int id)
        {
            var existingPatient = await _context.Patients
                .Include(p => p.PatientGuardians)
                .Include(p => p.PatientDoctors)
                .Include(p => p.MedicalHistory)
                .Include(p => p.Reports)
                .Include(p => p.Tool)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existingPatient == null)
            {
                return null;
            }

            try
            {
                // Remove related entities
                await RemoveRelatedEntities(existingPatient);

                // Now remove the patient
                _context.Patients.Remove(existingPatient);
                await _context.SaveChangesAsync();

                return existingPatient;
            }
            catch (Exception ex)
            {
                // Log the exception
                // Log.Error(ex, "An error occurred while deleting the patient.");
                return null;
            }
        }
        private async Task RemoveRelatedEntities(Patient patient)
        {
            //TODO: Add more related entities to remove
            if (patient.PatientGuardians != null && patient.PatientGuardians.Any())
            {
                _context.PatientGuardians.RemoveRange(patient.PatientGuardians);
            }

            if (patient.PatientDoctors != null && patient.PatientDoctors.Any())
            {
                var patientDoctors = _context.PatientDoctors.Where(pd => pd.PatientId == patient.Id);
                _context.PatientDoctors.RemoveRange(patientDoctors);
            }

            if (patient.Reports != null && patient.Reports.Any())
            {
                _context.Reports.RemoveRange(patient.Reports);
            }

            if (patient.Notifications != null && patient.Notifications.Any())
            {
                _context.Notifications.RemoveRange(patient.Notifications);
            }

            if (patient.Emergencies != null && patient.Emergencies.Any())
            {
                _context.Emergencies.RemoveRange(patient.Emergencies);
            }

            if (patient.MedicalHistory != null)
            {
                _context.MedicalHistory.Remove(patient.MedicalHistory);
            }

            if (patient.Tool != null)
            {
                _context.Tools.Remove(patient.Tool);
            }

            await _context.SaveChangesAsync();
        }
        public async Task<List<Patient>> GetAll(int pageNumber = 1, int pageSize = 1000)
        {
            var skipresults = (pageNumber - 1) * pageSize;
            return await _context.Patients.Skip(skipresults).Take(pageSize).ToListAsync();
        }
        public async Task<Patient?> GetById(int id)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(x => x.Id == id);
            return patient;
        }
        public async Task<Patient> GetByName(string name)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.Name == name);
        }
        public async Task<List<GetAllDoctorsForPatientDto>> GetDoctorsForPatient(int id)
        {
            var patient = await _context.Patients
              .Where(x => x.Id == id)
              .Include(x => x.PatientDoctors)
              .ThenInclude(pd => pd.Doctor) 
              .FirstOrDefaultAsync();

            if (patient == null)
            {
                return null; 
            }

            return patient.PatientDoctors
                .Select(pd => new GetAllDoctorsForPatientDto
                {
                    Name = pd.Doctor.Name
                })
                .ToList();
        }
        public async Task<List<GetAllGuardiansDto>> GetAllGuardiansForPatient(int id)
        {
            var patient = await _context.Patients
                .Where(x => x.Id == id)
                .Include(x => x.PatientGuardians)
                .ThenInclude(pg => pg.Guardian)
                .FirstOrDefaultAsync();

            if (patient == null)
            {
                return null;
            }

            return patient.PatientGuardians
                .Select(x => new GetAllGuardiansDto
                {
                    Name = x.Guardian.Name,
                    PhoneNumber = x.Guardian.PhoneNumber,
                    Relationship = x.Guardian.Relationship
                })
                .ToList();
        }
        public async Task<GetMedicalHistoryDto> GetMedicalHistoryForPatient(int id)
        {
            var patient = await _context.Patients
                .Where(x => x.Id == id)
                .Include(x => x.MedicalHistory)
                .FirstOrDefaultAsync();

            if (patient == null || patient.MedicalHistory == null)
            {
                return null;
            }

            return new GetMedicalHistoryDto
            {
                Diagonsis = patient.MedicalHistory.Diagnosis,
                Id = patient.MedicalHistory.Id,
                Medication = patient.MedicalHistory.Medication
            };
        }
        public async Task<List<GetAllReportsDto>> GetReportsForPatient(int id)
        {
            var patient = await _context.Patients
                .Where(x => x.Id == id)
                .Include(x => x.Reports)
                .FirstOrDefaultAsync();

            if (patient == null)
            {
                return null;
            }

            return patient.Reports
                .Select(x => new GetAllReportsDto
                {
                    Id = x.Id,
                    ReportDetails = x.ReportDetails,
                    UploadDate = x.UploadDate
                })
                .ToList();
        }
        public async Task<PatientDetailsDto> GetPatientDetails(int id)
        {
            var patient = await _context.Patients
                .Where(x => x.Id == id)
                .Include(x => x.PatientGuardians)
                .Include(x => x.MedicalHistory)
                .Include(x => x.Reports)
                .FirstOrDefaultAsync();

            if (patient == null)
            {
                return null;
            }

            return new PatientDetailsDto
            {
                Id = patient.Id,
                Name = patient.Name,
                SSN = patient.SSN,
                Gender = patient.Gender,

                Guardians = patient.PatientGuardians.Select(x => new GetAllGuardiansDto
                {
                    Name = x.Guardian.Name,
                    PhoneNumber = x.Guardian.PhoneNumber,
                    Relationship = x.Guardian.Relationship
                }).ToList(),

                MedicalHistory = patient.MedicalHistory == null ? null
                : new GetMedicalHistoryDto
                {
                    Id = patient.MedicalHistory.Id,
                    Diagonsis = patient.MedicalHistory.Diagnosis,
                    Medication = patient.MedicalHistory.Medication
                },

                Reports = patient.Reports.Select(x => new GetAllReportsDto
                {
                    Id = x.Id,
                    UploadDate = x.UploadDate,
                    ReportDetails = x.ReportDetails
                }).ToList()
            };
        }
    }
}
