using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NABD.Models.Domain;
using NABD.Repositores;

namespace NABD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmergencyController : ControllerBase
    {
        private readonly IEmergencyRepository emergencyRepository;
        private readonly IMapper mapper;

        public EmergencyController(IEmergencyRepository emergencyRepository, IMapper mapper)
        {
            this.emergencyRepository = emergencyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumer = 1, int pageSize = 1000)
        {
            var emergencies = await emergencyRepository.GetAll(pageNumer, pageSize);
            return Ok(emergencies);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var emergency = await emergencyRepository.GetById(id);
            return Ok(emergency);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var emergencyModel = await emergencyRepository.Delete(id);
            if (emergencyModel == null)
            {
                return NotFound($"the emergency with id {id} not found!");
            }

            var emergencyDto = mapper.Map<Emergency>(emergencyModel);
            return Ok(emergencyDto);
        }
    }
}
