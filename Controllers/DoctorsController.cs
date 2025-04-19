using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NABD.DTO;
using NABD.Models.Domain;
using NABD.Repositores;

namespace NABD.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public DoctorsController(IDoctorRepository doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] AddDoctorDto adddoctorDto)
        {
            var doctormodel = _mapper.Map<Doctor>(adddoctorDto);
            await _doctorRepository.AddDoctorAsync(doctormodel);

            var patientdto = _mapper.Map<DoctorDto>(doctormodel);
            return Ok(patientdto);
        }

        [HttpGet]
        public async Task<ActionResult<List<GetDoctorDto>>> GetAllDoctors()
        {
            var doctors = await _doctorRepository.GetAllDoctorsAsync();
            return Ok(doctors);
        }

        [HttpGet("{doctorId}")]
        public async Task<ActionResult<GetDoctorDto>> GetDoctorById(int doctorId)
        {
            var doctor = await _doctorRepository.GetDoctorByIdAsync(doctorId);
            if (doctor == null) return NotFound();

            return Ok(_mapper.Map<GetDoctorDto>(doctor));
        }

        [HttpPut("{doctorId}")]
        public async Task<IActionResult> UpdateDoctor(int doctorId, [FromBody] UpdateDoctorDto doctorDto)
        {
            var doctor = await _doctorRepository.GetDoctorByIdAsync(doctorId);
            if (doctor == null) return NotFound();

            _mapper.Map(doctorDto, doctor);
            _doctorRepository.UpdateDoctor(doctor);
            await _doctorRepository.SaveChangesAsync();

            return Ok(doctor);
        }

        [HttpDelete("{doctorId}")]
        public async Task<IActionResult> DeleteDoctor(int doctorId)
        {
            await _doctorRepository.DeleteDoctorAsync(doctorId);
            await _doctorRepository.SaveChangesAsync();

            return Ok("Doctor deleted successfully");
        }
    }
}
