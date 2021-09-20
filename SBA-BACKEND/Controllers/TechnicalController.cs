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
 
 namespace SBA_BACKEND.Controllers
 {
 	[Route("api/technical")]
 	[ApiController]
 	public class TechnicalController : ControllerBase
 	{
 		private readonly ITechnicalService _technicalService;
 		private readonly IMapper _mapper;
 
 		public TechnicalController(ITechnicalService technicalService, IMapper mapper)
 		{
 			_technicalService = technicalService;
 			_mapper = mapper;
 		}
 
 		[SwaggerOperation(Tags = new[] { "technicals" })]
 		[HttpPost]
 		[ProducesResponseType(typeof(TechnicalResource), 200)]
 		[ProducesResponseType(typeof(BadRequestResult), 404)]
 		public async Task<IActionResult> PostAsync([FromBody] SaveTechnicalResource resource)
 		{
 			if (!ModelState.IsValid)
 				return BadRequest(ModelState.GetErrorMessages());
 			var technical = _mapper.Map<SaveTechnicalResource, Technical>(resource);
 			var result = await _technicalService.SaveAsync(technical);
 
 			if (!result.Success)
 				return BadRequest(result.Message);
 			var technicalResource = _mapper.Map<Technical, TechnicalResource>(result.Resource);
 			return Ok(technicalResource);
 		}
 
 		[SwaggerOperation(Tags = new[] { "technicals" })]
 		[HttpPut("{technicalId}")]
 		[ProducesResponseType(typeof(TechnicalResource), 200)]
 		[ProducesResponseType(typeof(BadRequestResult), 404)]
 		public async Task<IActionResult> PutAsync(int technicalId, [FromBody] SaveTechnicalResource resource)
 		{
 			if (!ModelState.IsValid)
 				return BadRequest(ModelState.GetErrorMessages());
 
 			var technical = _mapper.Map<SaveTechnicalResource, Technical>(resource);
 			var result = await _technicalService.UpdateAsync(technicalId, technical);
 
 			if (!result.Success)
 				return BadRequest(result.Message);
 			var technicalResource = _mapper.Map<Technical, TechnicalResource>(result.Resource);
 			return Ok(technicalResource);
 		}
 
 		[SwaggerOperation(Tags = new[] { "technicals" })]
 		[HttpGet("{technicalId}")]
 		[ProducesResponseType(typeof(TechnicalResource), 200)]
 		[ProducesResponseType(typeof(BadRequestResult), 404)]
 		public async Task<IActionResult> GetAsync(int technicalId)
 		{
 			var result = await _technicalService.GetByIdAsync(technicalId);
 
 			if (!result.Success)
 				return BadRequest(result.Message);
 
 			var technicalResource = _mapper.Map<Technical, TechnicalResource>(result.Resource);
 
 			return Ok(technicalResource);
 		}
 
 		[SwaggerOperation(Tags = new[] { "technicals" })]
 		[HttpDelete("{technicalId}")]
 		[ProducesResponseType(typeof(TechnicalResource), 200)]
 		[ProducesResponseType(typeof(BadRequestResult), 404)]
 		public async Task<IActionResult> DeleteAsync(int technicalId)
 		{
 			var result = await _technicalService.DeleteAsync(technicalId);
 			if (!result.Success)
 				return BadRequest(result.Message);
 			var technicalResource = _mapper.Map<Technical, TechnicalResource>(result.Resource);
 			return Ok(technicalResource);
 		}
 	}
 }
