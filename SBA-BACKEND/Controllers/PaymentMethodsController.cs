using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBA_BACKEND.Domain.Models;
using SBA_BACKEND.Domain.Services;
using SBA_BACKEND.Resources;
using SBA_BACKEND.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBA_BACKEND.Controllers
{
    [Authorize]
    [Route("api/paymentMethod")]
    [ApiController]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly IMapper _mapper;

        public PaymentMethodsController(IPaymentMethodService paymentMethodService, IMapper mapper)
        {
            _paymentMethodService = paymentMethodService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all payment methods",
            Description = "List of Payment Methods",
            OperationId = "ListAllPaymentMethods",
            Tags = new[] { "paymentMethods" })]
        [SwaggerResponse(200, "List of PaymentMethods", typeof(IEnumerable<PaymentMethodsController>))]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PaymentMethodResource>), 200)]
        public async Task<IEnumerable<PaymentMethodResource>> GetAllAsync()
        {
            var paymentMethods = await _paymentMethodService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<PaymentMethod>, IEnumerable<PaymentMethodResource>>(paymentMethods);
            return resources;
        }
    }
}
