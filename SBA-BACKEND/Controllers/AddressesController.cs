using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using SBA_BACKEND.Domain.Models;
using SBA_BACKEND.Domain.Services;
using SBA_BACKEND.Resources;
using SBA_BACKEND.API.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace SBA_BACKEND.Controllers
{
    [Authorize]
    [Route("api/address")]
 	[ApiController]
 	public class AddressesController : ControllerBase
 	{
 		private readonly IAddressService _addressService;
 		private readonly IMapper _mapper;
 
 		public AddressesController(IAddressService addressService, IMapper mapper)
 		{
 			_addressService = addressService;
 			_mapper = mapper;
 		}

        [SwaggerOperation(
            Summary = "List all addresses",
            Description = "List of Addresss",
            OperationId = "ListAllAddresss",
            Tags = new[] { "addresses" })]
        [SwaggerResponse(200, "List of Addresss", typeof(IEnumerable<AddressResource>))]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AddressResource>), 200)]
        public async Task<IEnumerable<AddressResource>> GetAllAsync()
        {
            var addresses = await _addressService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<Address>, IEnumerable<AddressResource>>(addresses);
            return resources;
        }

        [SwaggerOperation(Tags = new[] { "addresses" })]
 		[HttpPost]
 		[ProducesResponseType(typeof(AddressResource), 200)]
 		[ProducesResponseType(typeof(BadRequestResult), 404)]
 		public async Task<IActionResult> PostAsync(int userId, [FromBody] SaveAddressResource resource)
 		{
 			if (!ModelState.IsValid)
 				return BadRequest(ModelState.GetErrorMessages());
 			var address = _mapper.Map<SaveAddressResource, Address>(resource);
 			var result = await _addressService.SaveAsync(userId, address);
 
 			if (!result.Success)
 				return BadRequest(result.Message);
 			var addressResource = _mapper.Map<Address, AddressResource>(result.Resource);
 			return Ok(addressResource);
 		}
 
 		[SwaggerOperation(Tags = new[] { "addresses" })]
 		[HttpPut("{userId}")]
 		[ProducesResponseType(typeof(AddressResource), 200)]
 		[ProducesResponseType(typeof(BadRequestResult), 404)]
 		public async Task<IActionResult> PutAsync(int userId, [FromBody] SaveAddressResource resource)
 		{
 			if (!ModelState.IsValid)
 				return BadRequest(ModelState.GetErrorMessages());
 
 			var address = _mapper.Map<SaveAddressResource, Address>(resource);
 			var result = await _addressService.UpdateAsync(userId, address);
 
 			if (!result.Success)
 				return BadRequest(result.Message);
 			var addressResource = _mapper.Map<Address, AddressResource>(result.Resource);
 			return Ok(addressResource);
 		}
 
 		[SwaggerOperation(Tags = new[] { "addresses" })]
 		[HttpGet("{userId}")]
 		[ProducesResponseType(typeof(AddressResource), 200)]
 		[ProducesResponseType(typeof(BadRequestResult), 404)]
 		public async Task<IActionResult> GetAsync(int userId)
 		{
 			var result = await _addressService.GetByIdAsync(userId);
 
 			if (!result.Success)
 				return BadRequest(result.Message);
 
 			var addressResource = _mapper.Map<Address, AddressResource>(result.Resource);
 
 			return Ok(addressResource);
 		}
 
 		[SwaggerOperation(Tags = new[] { "addresses" })]
 		[HttpDelete("{userId}")]
 		[ProducesResponseType(typeof(AddressResource), 200)]
 		[ProducesResponseType(typeof(BadRequestResult), 404)]
 		public async Task<IActionResult> DeleteAsync(int userId)
 		{
 			var result = await _addressService.DeleteAsync(userId);
 			if (!result.Success)
 				return BadRequest(result.Message);
 			var addressResource = _mapper.Map<Address, AddressResource>(result.Resource);
 			return Ok(addressResource);
 		}
 	}
 }
