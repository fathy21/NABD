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
    public class ToolsController : ControllerBase
    {
        private readonly IToolRepository _toolRepository;
        private readonly IMapper _mapper;

        public ToolsController(IToolRepository toolRepository, IMapper mapper)
        {
            _toolRepository = toolRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToolDto>>> GetTools()
        {
            var tools = await _toolRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ToolDto>>(tools));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToolDto>> GetTool(int id)
        {
            var tool = await _toolRepository.GetByIdAsync(id);
            if (tool == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ToolDto>(tool));
        }

        [HttpPost]
        public async Task<ActionResult<ToolDto>> CreateTool(CreateToolDto createToolDto)
        {
            var tool = _mapper.Map<Tool>(createToolDto);
            var newTool = await _toolRepository.CreateAsync(tool);
            return CreatedAtAction(nameof(GetTool), new { id = newTool.Id }, _mapper.Map<ToolDto>(newTool));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTool([FromRoute]int id, CreateToolDto updateToolDto)
        {
            var toolModel = _mapper.Map<Tool>(updateToolDto);
            toolModel = await _toolRepository.UpdateAsync(id,toolModel);

            if (toolModel == null)
            {
                return NotFound();
            }

            var toolDto = _mapper.Map<ToolDto>(toolModel);
            return Ok(toolDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTool(int id)
        {
            bool deleted = await _toolRepository.DeleteAsync(id);
            return deleted ? Ok("Tool deleted successfully") : NotFound();
        }
    }
}
