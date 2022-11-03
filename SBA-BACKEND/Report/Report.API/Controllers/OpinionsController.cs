using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using SBA_BACKEND.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using SBA_BACKEND.Report.Report.Domain.AgreggatesModel;
using SBA_BACKEND.Report.Report.API.Services;
using SBA_BACKEND.Report.Report.API.Resources;

namespace SBA_BACKEND.Report.Report.API.Controllers
{
    [Authorize]
    [Route("api/opinion")]
    [ApiController]
    public class OpinionsController : ControllerBase
    {
        private readonly IOpinionService _opinionService;
        private readonly IMapper _mapper;

        public OpinionsController(IOpinionService opinionService, IMapper mapper)
        {
            _opinionService = opinionService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all opinions",
            Description = "List of Opinions",
            OperationId = "ListAllOpinions",
            Tags = new[] { "opinions" })]
        [SwaggerResponse(200, "List of Opinions", typeof(IEnumerable<OpinionResource>))]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OpinionResource>), 200)]
        public async Task<IEnumerable<OpinionResource>> GetAllAsync()
        {
            var opinions = await _opinionService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<Opinion>, IEnumerable<OpinionResource>>(opinions);
            return resources;
        }

        [SwaggerOperation(Tags = new[] { "opinions" })]
        [HttpPost]
        [ProducesResponseType(typeof(OpinionResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int customerId, int technicianId, [FromBody] SaveOpinionResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var opinion = _mapper.Map<SaveOpinionResource, Opinion>(resource);
            var result = await _opinionService.SaveAsync(customerId, technicianId, opinion);

            if (!result.Success)
                return BadRequest(result.Message);
            var opinionResource = _mapper.Map<Opinion, OpinionResource>(result.Resource);
            return Ok(opinionResource);
        }

        [SwaggerOperation(Tags = new[] { "opinions" })]
        [HttpPut("{opinionId}")]
        [ProducesResponseType(typeof(OpinionResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int opinionId, [FromBody] SaveOpinionResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var opinion = _mapper.Map<SaveOpinionResource, Opinion>(resource);
            var result = await _opinionService.UpdateAsync(opinionId, opinion);

            if (!result.Success)
                return BadRequest(result.Message);
            var opinionResource = _mapper.Map<Opinion, OpinionResource>(result.Resource);
            return Ok(opinionResource);
        }

        [SwaggerOperation(Tags = new[] { "opinions" })]
        [HttpGet("{opinionId}")]
        [ProducesResponseType(typeof(OpinionResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int opinionId)
        {
            var result = await _opinionService.GetByIdAsync(opinionId);

            if (!result.Success)
                return BadRequest(result.Message);

            var opinionResource = _mapper.Map<Opinion, OpinionResource>(result.Resource);

            return Ok(opinionResource);
        }

        [SwaggerOperation(Tags = new[] { "opinions" })]
        [HttpDelete("{opinionId}")]
        [ProducesResponseType(typeof(OpinionResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int opinionId)
        {
            var result = await _opinionService.DeleteAsync(opinionId);
            if (!result.Success)
                return BadRequest(result.Message);
            var opinionResource = _mapper.Map<Opinion, OpinionResource>(result.Resource);
            return Ok(opinionResource);
        }
    }
}
