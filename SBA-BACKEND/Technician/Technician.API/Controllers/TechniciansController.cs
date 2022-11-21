using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using SBA_BACKEND.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using SBA_BACKEND.Technician.Technician.API.Services;
using SBA_BACKEND.Technician.Technician.API.Resources;

namespace SBA_BACKEND.Technician.Technician.API.Controllers
{
    [Authorize]
    [Route("api/technician")]
    [ApiController]
    public class TechniciansController : ControllerBase
    {
        private readonly ITechnicianService _technicianService;
        private readonly IMapper _mapper;

        public TechniciansController(ITechnicianService technicianService, IMapper mapper)
        {
            _technicianService = technicianService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all technicians",
            Description = "List of Technicians",
            OperationId = "ListAllTechnicians",
            Tags = new[] { "technicians" })]
        [SwaggerResponse(200, "List of Technicians", typeof(IEnumerable<TechnicianResource>))]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TechnicianResource>), 200)]
        public async Task<IEnumerable<TechnicianResource>> GetAllAsync()
        {
            var technicians = await _technicianService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<Technician.Domain.AgreggatesModel.Technician>, IEnumerable<TechnicianResource>>(technicians);
            return resources;
        }

        [SwaggerOperation(
            Summary = "List all technicians by specialtyId",
            Description = "List of Technicians by specialtyId",
            OperationId = "ListTechniciansBySpecialtyId",
            Tags = new[] { "technicians" })]
        [SwaggerResponse(200, "List of Technicians", typeof(IEnumerable<TechnicianResource>))]
        [AllowAnonymous]
        [HttpGet, Route("GetTechniciansBySpecialtyId/{specialtyId}")]
        [ProducesResponseType(typeof(IEnumerable<TechnicianResource>), 200)]
        public async Task<IEnumerable<TechnicianResource>> GetTechniciansBySpecialtyIdAsync(int specialtyId)
        {
            var technicians = await _technicianService.ListTechniciansBySpecialtyId(specialtyId);
            var resources = _mapper
                .Map<IEnumerable<Technician.Domain.AgreggatesModel.Technician>, IEnumerable<TechnicianResource>>(technicians);
            return resources;
        }

        [AllowAnonymous]
        [SwaggerOperation(Tags = new[] { "technicians" })]
        [HttpPost("{userId}")]
        [ProducesResponseType(typeof(TechnicianResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int userId, [FromBody] SaveTechnicianResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var technician = _mapper.Map<SaveTechnicianResource, Technician.Domain.AgreggatesModel.Technician>(resource);
            var result = await _technicianService.SaveAsync(userId, technician);

            if (!result.Success)
                return BadRequest(result.Message);
            var technicianResource = _mapper.Map<Technician.Domain.AgreggatesModel.Technician, TechnicianResource>(result.Resource);
            return Ok(technicianResource);
        }

        [SwaggerOperation(Tags = new[] { "technicians" })]
        [HttpPut("{userId}")]
        [ProducesResponseType(typeof(TechnicianResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int userId, [FromBody] SaveTechnicianResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var technician = _mapper.Map<SaveTechnicianResource, Technician.Domain.AgreggatesModel.Technician>(resource);
            var result = await _technicianService.UpdateAsync(userId, technician);

            if (!result.Success)
                return BadRequest(result.Message);
            var technicianResource = _mapper.Map<Technician.Domain.AgreggatesModel.Technician, TechnicianResource>(result.Resource);
            return Ok(technicianResource);
        }

        [SwaggerOperation(Tags = new[] { "technicians" })]
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(TechnicianResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int userId)
        {
            var result = await _technicianService.GetByIdAsync(userId);

            if (!result.Success)
                return BadRequest(result.Message);

            var technicianResource = _mapper.Map<Technician.Domain.AgreggatesModel.Technician, TechnicianResource>(result.Resource);

            return Ok(technicianResource);
        }

        [SwaggerOperation(Tags = new[] { "technicians" })]
        [HttpDelete("{userId}")]
        [ProducesResponseType(typeof(TechnicianResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int userId)
        {
            var result = await _technicianService.DeleteAsync(userId);
            if (!result.Success)
                return BadRequest(result.Message);
            var technicianResource = _mapper.Map<Technician.Domain.AgreggatesModel.Technician, TechnicianResource>(result.Resource);
            return Ok(technicianResource);
        }
    }
}
