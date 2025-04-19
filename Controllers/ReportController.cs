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
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository reportRepsitory;
        private readonly IMapper mapper;

        public ReportController(IReportRepository reportRepsitory, IMapper mapper)
        {
            this.reportRepsitory = reportRepsitory;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 1000)
        {
            var reports = await reportRepsitory.GetAll(pageNumber, pageSize);
            return Ok(reports);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var report = await reportRepsitory.GetById(id);
            return Ok(report);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddReportDto addReport)
        {
            var reportModel = mapper.Map<Report>(addReport);
            await reportRepsitory.Create(reportModel);

            var reportDto = mapper.Map<Report>(reportModel);
            return Ok(reportDto);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateReportDto updateReport)
        {
            var updatedReport = await reportRepsitory.Update(id, mapper.Map<Report>(updateReport));

            if (updatedReport == null)
            {
                return NotFound($"Report with ID {id} not found!");
            }

            var reportDto = mapper.Map<UpdateReportDto>(updatedReport);
            return Ok(reportDto);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var report = await reportRepsitory.Delete(id);
            if (report == null)
            {
                return NotFound($"Report with ID {id} not found!");
            }

            var reportDto = mapper.Map<Report>(report);
            return Ok(reportDto);
        }
    }
}
