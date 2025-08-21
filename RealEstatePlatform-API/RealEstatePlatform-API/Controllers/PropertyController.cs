using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RealEstatePlatform_API.DTOs.Property;
using RealEstatePlatform_API.Repositories.Interfaces;

namespace RealEstatePlatform_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyRepository  _propertyRepository;
        private readonly IMapper _mapper;

        public PropertyController(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        // GET: api/property
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll()
        {
            var properties = await _propertyRepository.GetPropertiesAsync();
            var result =  _mapper.Map<IEnumerable<PropertyDTO>>(properties);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid property ID");

            var property = await _propertyRepository.GetPropertyByIdAsync(id);
            if (property == null)
                return NotFound($"Property with ID {id} not found");

            var result = _mapper.Map<PropertyDTO>(property);
            return Ok(result);
        }

        [HttpGet("agent/{agentId}")]
        public async Task<IActionResult> GetProperiesByAgent(string agentId)
        {
            var properties = await _propertyRepository.GetPropertiesByAgentIdAsync(agentId);
            var result = _mapper.Map<IEnumerable<PropertyDTO>>(properties);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> Create([FromBody] PropertyDTO propertyDto)
        {
            if (propertyDto == null)
            {
                return BadRequest("Property data is null");
            }
            var property = _mapper.Map<Models.Property>(propertyDto);
            await _propertyRepository.AddAsync(property);
            return CreatedAtAction(nameof(GetById), new { id = property.Id }, propertyDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> Update(int id, [FromBody] PropertyUpdateDTO propertyDTO)
        {
            if (propertyDTO == null)
            {
                return BadRequest(new { message = "Property data is null or ID mismatch" });
            }
            var existingProperty = await _propertyRepository.GetPropertyByIdAsync(id);
            if (existingProperty == null)
            {
                return NotFound(new {message=$"Property with id {id} Not Found"});
            }

            await _propertyRepository.UpdateAsync(id,propertyDTO);
            return Ok(new {message="Upated successfully"});

        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingProperty = await _propertyRepository.GetPropertyByIdAsync(id);
            if (existingProperty == null)
            {
                return NotFound(new { message = $"Property with id {id} Not Found" });
            }
            await _propertyRepository.DeleteAsync(id);
            return Ok(new { message = "Deleted successfully" });

        }

    }
}
