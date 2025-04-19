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
    public class GuardiansController : ControllerBase
    {
        private readonly IGuardianRepository guardianRepository;
        private readonly IMapper mapper;

        public GuardiansController(IGuardianRepository gurdianRepository, IMapper mapper)
        {
            this.guardianRepository = gurdianRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, int pageSize = 1000)
        {
            var gurdians = await guardianRepository.GetAll(pageNumber, pageSize);
            return Ok(gurdians);
        }

        [HttpGet]
        [Route("ByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var gurdianByName = await guardianRepository.GetByName(name);
            return Ok(gurdianByName);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var gurdianById = await guardianRepository.GetById(id);
            return Ok(gurdianById);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddGuardianDto addGurdianDto,int patientId)
        {
            var guardian = mapper.Map<Guardian>(addGurdianDto);
            await guardianRepository.Create(guardian, patientId);

            var gurdiandto = mapper.Map<GuardianDto>(guardian);
            return Ok(gurdiandto);
        }

        [HttpGet]
        [Route("{id}/Emergencies")]
        public async Task<IActionResult> getAllEmergenciesforGurdians(int id)
        {
            var gurdianModel = await guardianRepository.getAllEmergenciesforGuardians(id);

            if (gurdianModel == null || !gurdianModel.Any())
            {
                return NotFound($"gurdian with id {id} not found!");
            }

            var gurdiandto = mapper.Map<List<GetAllEmergenciesForGuardianDto>>(gurdianModel);
            return Ok(gurdiandto);
        }

        [HttpGet]
        [Route("{id}/Notifications")]
        public async Task<IActionResult> getAllNotificationsforGurdians(int id)
        {
            var gurdianModel = await guardianRepository.getAllNotificationsforGuardians(id);

            if (gurdianModel == null || !gurdianModel.Any())
            {
                return NotFound($"gurdian with id {id} not found!");
            }

            var gurdiandto = mapper.Map<List<GetAllNotificationsForGuardianDto>>(gurdianModel);
            return Ok(gurdiandto);
        }

        [HttpGet]
        [Route("{id}/Patient")]
        public async Task<IActionResult> getPatientByGurdianId(int id)
        {
            var gurdianModel = await guardianRepository.getPatientByGuardianID(id);

            if (gurdianModel == null)
            {
                return NotFound($"the gurdian with id {id} not found!");
            }

            var gurdiandto = mapper.Map<GetPatientByGuardianIdDto>(gurdianModel);
            return Ok(gurdiandto);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateGuardianDto updateGurdianDto)
        {
            var gurdianModel = mapper.Map<Guardian>(updateGurdianDto);
            gurdianModel = await guardianRepository.Update(id, gurdianModel);

            if (gurdianModel == null)
            {
                return NotFound();
            }

            var gurdianDto = mapper.Map<GuardianDto>(gurdianModel);
            return Ok(gurdianDto);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var gurdianModel = await guardianRepository.Delete(id);

            if (gurdianModel == null)
            {
                return NotFound();
            }

            var gurdianDto = mapper.Map<GuardianDto>(gurdianModel);
            return Ok("Gurdian deleted successfully");
        }
    }
}
