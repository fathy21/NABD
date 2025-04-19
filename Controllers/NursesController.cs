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
    public class NursesController : ControllerBase
    {
        private readonly INurseRepository _nurseRepository;
        private readonly IMapper _mapper;

        public NursesController(INurseRepository nurseRepository, IMapper mapper)
        {
            _nurseRepository = nurseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetNurseDto>>> GetAllNurses()
        {
            var nurses = await _nurseRepository.GetAllNursesAsync();
            return Ok(_mapper.Map<List<GetNurseDto>>(nurses));
        }

        [HttpGet("{nurseId}")]
        public async Task<ActionResult<GetNurseDto>> GetNurseById(int nurseId)
        {
            var nurse = await _nurseRepository.GetNurseByIdAsync(nurseId);
            if (nurse == null) return NotFound();

            return Ok(_mapper.Map<GetNurseDto>(nurse));
        }

        [HttpPost]
        public async Task<ActionResult<GetNurseDto>> AddNurse(AddNurseDto nurseDto)
        {
            var nurse = _mapper.Map<Nurse>(nurseDto);
            await _nurseRepository.AddNurseAsync(nurse);
            await _nurseRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNurseById), new { nurseId = nurse.Id }, _mapper.Map<GetNurseDto>(nurse));
        }

        [HttpPut("{nurseId}")]
        public async Task<IActionResult> UpdateNurse(int nurseId, UpdateNurseDto nurseDto)
        {
            var nurse = await _nurseRepository.GetNurseByIdAsync(nurseId);
            if (nurse == null) return NotFound();

            _mapper.Map(nurseDto, nurse);
            _nurseRepository.UpdateNurse(nurse);
            await _nurseRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{nurseId}")]
        public async Task<IActionResult> DeleteNurse(int nurseId)
        {
            var nurse = await _nurseRepository.GetNurseByIdAsync(nurseId);
            if (nurse == null) return NotFound();

            await _nurseRepository.DeleteNurseAsync(nurseId);
            await _nurseRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
