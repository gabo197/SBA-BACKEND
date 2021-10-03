using AutoMapper;
 using Microsoft.AspNetCore.Http;
 using Microsoft.AspNetCore.Mvc;
 using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Threading.Tasks;
 using Swashbuckle.AspNetCore.Annotations;
 using SBA_BACKEND.Domain.Models;
 using SBA_BACKEND.Domain.Services;
 using SBA_BACKEND.Resources;
 using SBA_BACKEND.API.Extensions;
 using Swashbuckle.Swagger;
using Microsoft.AspNetCore.Authorization;

namespace SBA_BACKEND.Controllers
 {
    [Authorize]
    [Route("api/specialty")]
 	[ApiController]
 	public class SpecialtiesController : ControllerBase
 	{
 		private readonly ISpecialtyService _specialtyService;
 		private readonly IMapper _mapper;
 
 		public SpecialtiesController(ISpecialtyService specialtyService, IMapper mapper)
 		{
 			_specialtyService = specialtyService;
 			_mapper = mapper;
 		}

        [SwaggerOperation(
            Summary = "List all specialties",
            Description = "List of Specialties",
            OperationId = "ListAllSpecialties",
            Tags = new[] { "specialties" })]
        [SwaggerResponse(200, "List of Specialties", typeof(IEnumerable<SpecialtyResource>))]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SpecialtyResource>), 200)]
        public async Task<IEnumerable<SpecialtyResource>> GetAllAsync()
        {
            var specialties = await _specialtyService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<Specialty>, IEnumerable<SpecialtyResource>>(specialties);
            return resources;
        }

        [SwaggerOperation(Tags = new[] { "specialties" })]
 		[HttpPost]
 		[ProducesResponseType(typeof(SpecialtyResource), 200)]
 		[ProducesResponseType(typeof(BadRequestResult), 404)]
 		public async Task<IActionResult> PostAsync([FromBody] SaveSpecialtyResource resource)
 		{
 			if (!ModelState.IsValid)
 				return BadRequest(ModelState.GetErrorMessages());
 			var specialty = _mapper.Map<SaveSpecialtyResource, Specialty>(resource);
 			var result = await _specialtyService.SaveAsync(specialty);
 
 			if (!result.Success)
 				return BadRequest(result.Message);
 			var specialtyResource = _mapper.Map<Specialty, SpecialtyResource>(result.Resource);
 			return Ok(specialtyResource);
 		}
 
 		[SwaggerOperation(Tags = new[] { "specialties" })]
 		[HttpPut("{specialtyId}")]
 		[ProducesResponseType(typeof(SpecialtyResource), 200)]
 		[ProducesResponseType(typeof(BadRequestResult), 404)]
 		public async Task<IActionResult> PutAsync(int specialtyId, [FromBody] SaveSpecialtyResource resource)
 		{
 			if (!ModelState.IsValid)
 				return BadRequest(ModelState.GetErrorMessages());
 
 			var specialty = _mapper.Map<SaveSpecialtyResource, Specialty>(resource);
 			var result = await _specialtyService.UpdateAsync(specialtyId, specialty);
 
 			if (!result.Success)
 				return BadRequest(result.Message);
 			var specialtyResource = _mapper.Map<Specialty, SpecialtyResource>(result.Resource);
 			return Ok(specialtyResource);
 		}
 
 		[SwaggerOperation(Tags = new[] { "specialties" })]
 		[HttpGet("{specialtyId}")]
 		[ProducesResponseType(typeof(SpecialtyResource), 200)]
 		[ProducesResponseType(typeof(BadRequestResult), 404)]
 		public async Task<IActionResult> GetAsync(int specialtyId)
 		{
 			var result = await _specialtyService.GetByIdAsync(specialtyId);
 
 			if (!result.Success)
 				return BadRequest(result.Message);
 
 			var specialtyResource = _mapper.Map<Specialty, SpecialtyResource>(result.Resource);
 
 			return Ok(specialtyResource);
 		}
 
 		[SwaggerOperation(Tags = new[] { "specialties" })]
 		[HttpDelete("{specialtyId}")]
 		[ProducesResponseType(typeof(SpecialtyResource), 200)]
 		[ProducesResponseType(typeof(BadRequestResult), 404)]
 		public async Task<IActionResult> DeleteAsync(int specialtyId)
 		{
 			var result = await _specialtyService.DeleteAsync(specialtyId);
 			if (!result.Success)
 				return BadRequest(result.Message);
 			var specialtyResource = _mapper.Map<Specialty, SpecialtyResource>(result.Resource);
 			return Ok(specialtyResource);
 		}
 	}
 }
