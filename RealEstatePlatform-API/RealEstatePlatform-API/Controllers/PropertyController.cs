using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var result =  _mapper.Map<IEnumerable<DTOs.PropertyDTO>>(properties);

            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var property = await _propertyRepository.GetPropertyByIdAsync(id);
            var result = _mapper.Map<DTOs.PropertyDTO>(property);

            return Ok(result);
        }

        
    }
}
