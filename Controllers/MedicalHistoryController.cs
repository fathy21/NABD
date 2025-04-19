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
    public class MedicalHistoryController : ControllerBase
    {
        private readonly IMedicalHistoryRepository medicalHistoryRepo;
        private readonly IMapper mapper;

        public MedicalHistoryController(IMedicalHistoryRepository medicalHistoryRepo, IMapper mapper)
        {
            this.medicalHistoryRepo = medicalHistoryRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 1000)
        {
            var medicalHistories = await medicalHistoryRepo.GetAll(pageNumber, pageSize);
            return Ok(medicalHistories);
        }

        [HttpGet]
        [Route("ById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var medicalHistory = await medicalHistoryRepo.GetById(id);
            return Ok(medicalHistory);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddMedicalHistoryDto addMedicalHistory)
        {
            var medicalHistoryModel = mapper.Map<MedicalHistory>(addMedicalHistory);
            await medicalHistoryRepo.Create(medicalHistoryModel);

            var medicalHistoryDto = mapper.Map<MedicalHistory>(medicalHistoryModel);
            return Ok(medicalHistoryDto);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var medicalHistoryModel = await medicalHistoryRepo.Delete(id);
            if (medicalHistoryModel == null)
            {
                return NotFound($"the medical history with {id} not found");
            }

            var medicalHistoryDto = mapper.Map<MedicalHistory>(medicalHistoryModel);
            return Ok(medicalHistoryDto);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateMedicalHistoryDto updateMedicalHistory)
        {
            var medicalHistory = mapper.Map<MedicalHistory>(updateMedicalHistory);
            var updatedMedicalHistory = await medicalHistoryRepo.Update(id, medicalHistory);

            if (updatedMedicalHistory == null)
            {
                return NotFound($"MedicalHistory with ID {id} not found!");
            }

            var medicalHistoryDto = mapper.Map<UpdateMedicalHistoryDto>(updatedMedicalHistory);
            return Ok(medicalHistoryDto);
        }
    }
}
