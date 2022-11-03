using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using SBA_BACKEND.Customer.Customer.Domain.AgreggatesModel;
using SBA_BACKEND.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using SBA_BACKEND.Customer.Customer.API.Services;
using SBA_BACKEND.Customer.Customer.API.Resources;

namespace SBA_BACKEND.Customer.Customer.API.Controllers
{
    [Authorize]
    [Route("api/customer")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all customers",
            Description = "List of Customers",
            OperationId = "ListAllCustomers",
            Tags = new[] { "customers" })]
        [SwaggerResponse(200, "List of Customers", typeof(IEnumerable<CustomerResource>))]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerResource>), 200)]
        public async Task<IEnumerable<CustomerResource>> GetAllAsync()
        {
            var customers = await _customerService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<Customer.Domain.AgreggatesModel.Customer>, IEnumerable<CustomerResource>>(customers);
            return resources;
        }

        [AllowAnonymous]
        [SwaggerOperation(Tags = new[] { "customers" })]
        [HttpPost("{userId}")]
        [ProducesResponseType(typeof(CustomerResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int userId, [FromBody] SaveCustomerResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var customer = _mapper.Map<SaveCustomerResource, Domain.AgreggatesModel.Customer>(resource);
            var result = await _customerService.SaveAsync(userId, customer);

            if (!result.Success)
                return BadRequest(result.Message);
            var customerResource = _mapper.Map<Domain.AgreggatesModel.Customer, CustomerResource>(result.Resource);
            return Ok(customerResource);
        }

        [SwaggerOperation(Tags = new[] { "customers" })]
        [HttpPut("{userId}")]
        [ProducesResponseType(typeof(CustomerResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int userId, [FromBody] SaveCustomerResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var customer = _mapper.Map<SaveCustomerResource, Domain.AgreggatesModel.Customer>(resource);
            var result = await _customerService.UpdateAsync(userId, customer);

            if (!result.Success)
                return BadRequest(result.Message);
            var customerResource = _mapper.Map<Domain.AgreggatesModel.Customer, CustomerResource>(result.Resource);
            return Ok(customerResource);
        }

        [SwaggerOperation(Tags = new[] { "customers" })]
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(CustomerResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int userId)
        {
            var result = await _customerService.GetByIdAsync(userId);

            if (!result.Success)
                return BadRequest(result.Message);

            var customerResource = _mapper.Map<Domain.AgreggatesModel.Customer, CustomerResource>(result.Resource);

            return Ok(customerResource);
        }

        [SwaggerOperation(Tags = new[] { "customers" })]
        [HttpDelete("{userId}")]
        [ProducesResponseType(typeof(CustomerResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int userId)
        {
            var result = await _customerService.DeleteAsync(userId);
            if (!result.Success)
                return BadRequest(result.Message);
            var customerResource = _mapper.Map<Domain.AgreggatesModel.Customer, CustomerResource>(result.Resource);
            return Ok(customerResource);
        }
    }
}
