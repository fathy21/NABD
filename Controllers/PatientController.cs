using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NABD.DTO;
using NABD.Models.Domain;
using NABD.Repositores;

namespace NABD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository patientrepo;
        private readonly IMapper mapper;

        public PatientController(IPatientRepository patientrepo, IMapper mapper)
        {
            this.patientrepo = patientrepo;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddPatientDto addPatientDto)
        {
            var patientmodel = mapper.Map<Patient>(addPatientDto);
            await patientrepo.Create(patientmodel);

            var patientdto = mapper.Map<PatientDto>(patientmodel);
            return Ok(patientdto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, int pageSize = 1000)
        {
            var patients = await patientrepo.GetAll(pageNumber, pageSize);
            return Ok(patients);
        }

        [HttpGet]
        [Route("ById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await patientrepo.GetById(id);
            return Ok(patient);
        }

        [HttpGet]
        [Route("ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var patient = await patientrepo.GetByName(name);
            return Ok(patient);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var patientmodel = await patientrepo.Delete(id);
            if (patientmodel == null)
            {
                return NotFound(new { Message = "Patient not found" });
            }

            var patientdto = mapper.Map<PatientDto>(patientmodel);
            return Ok("Patient deleted successfully");
        }

        [HttpGet]
        [Route("{patientid}/Doctors")]
        public async Task<IActionResult> GetAllDoctorsForPatient(int patientid)
        {
            var doctormodel = await patientrepo.GetDoctorsForPatient(patientid);

            if (doctormodel == null || !doctormodel.Any())
            {
                return NotFound("No medical staff found for the patient!");
            }

            var medicalStaffdto = mapper.Map<List<GetAllDoctorsForPatientDto>>(doctormodel);
            return Ok(medicalStaffdto);
        }

        [HttpGet]
        [Route("{id}/Guardians")]
        public async Task<IActionResult> GetAllGuardiansForPatient(int id)
        {
            var guardiansmodel = await patientrepo.GetAllGuardiansForPatient(id);
            if (guardiansmodel == null || !guardiansmodel.Any())
            {
                return NotFound("No Gurdian founded for the patient!");
            }

            var gurdiandto = mapper.Map<List<GetAllGuardiansDto>>(guardiansmodel);
            return Ok(gurdiandto);
        }

        [HttpGet]
        [Route("{id}/Medical-History")]
        public async Task<IActionResult> GetMedicalHistoryForPatient(int id)
        {
            var medicalhistorymodel = await patientrepo.GetMedicalHistoryForPatient(id);
            if (medicalhistorymodel == null)
            {
                return NotFound("No medical history founded for the patient!");
            }
            var medicalhistorydto = mapper.Map<GetMedicalHistoryDto>(medicalhistorymodel);
            return Ok(medicalhistorydto);
        }

        [HttpGet]
        [Route("{id}/Reports")]
        public async Task<IActionResult> GetAllReportsForPatient(int id)
        {
            var reportModel = await patientrepo.GetReportsForPatient(id);

            if (reportModel == null || !reportModel.Any())
            {
                return NotFound("No reports found for the patient!");
            }

            // Map the list of report models to a list of DTOs
            var reportsDto = mapper.Map<List<GetAllReportsDto>>(reportModel);

            return Ok(reportsDto);
        }

        [HttpGet]
        [Route("{id}/details")]
        public async Task<IActionResult> GetAllDetailsForPatient(int id)
        {
            var patientdetails = await patientrepo.GetPatientDetails(id);
            if (patientdetails == null)
            {
                return NotFound($"Patient with id {id} not founded!");
            }

            return Ok(patientdetails);
        }
    }
}
