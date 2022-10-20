using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBA_BACKEND.API.Extensions;
using SBA_BACKEND.Domain.Models;
using SBA_BACKEND.Domain.Services;
using SBA_BACKEND.Resources;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBA_BACKEND.Controllers
{
    [Authorize]
    [Route("api/appointment")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;

        public AppointmentsController(IAppointmentService appointmentService, IMapper mapper)
        {
            _appointmentService = appointmentService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all appointments",
            Description = "List of Appointments",
            OperationId = "ListAllAppointments",
            Tags = new[] { "appointments" })]
        [SwaggerResponse(200, "List of Appointments", typeof(IEnumerable<AppointmentResource>))]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AppointmentResource>), 200)]
        public async Task<IEnumerable<AppointmentResource>> GetAllAsync()
        {
            var appointments = await _appointmentService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<Appointment>, IEnumerable<AppointmentResource>>(appointments);
            return resources;
        }

        [SwaggerOperation(
            Summary = "List all appointments by customerId",
            Description = "List of Appointments filtered by customerId",
            OperationId = "ListAllAppointmentsByCustomerId",
            Tags = new[] { "appointments" })]
        [SwaggerResponse(200, "List of Appointments By CustomerId", typeof(IEnumerable<AppointmentResource>))]
        [AllowAnonymous]
        [HttpGet, Route("GetAppointmentsByCustomerId/{customerId}")]
        [ProducesResponseType(typeof(IEnumerable<AppointmentResource>), 200)]
        public async Task<IEnumerable<AppointmentResource>> GetAppointmentsByCustomerIdAsync(int customerId)
        {
            var appointments = await _appointmentService.GetByCustomerIdAsync(customerId);
            var resources = _mapper
                .Map<IEnumerable<Appointment>, IEnumerable<AppointmentResource>>(appointments);
            return resources;
        }

        [SwaggerOperation(
            Summary = "List all appointments by technicianId",
            Description = "List of Appointments filtered by technicianId",
            OperationId = "ListAllAppointmentsByTechnicianId",
            Tags = new[] { "appointments" })]
        [SwaggerResponse(200, "List of Appointments By TechnicianId", typeof(IEnumerable<AppointmentResource>))]
        [AllowAnonymous]
        [HttpGet, Route("GetAppointmentsByTechnicianId/{technicianId}")]
        [ProducesResponseType(typeof(IEnumerable<AppointmentResource>), 200)]
        public async Task<IEnumerable<AppointmentResource>> GetAppointmentsByTechnicianIdAsync(int technicianId)
        {
            var appointments = await _appointmentService.GetByTechnicianIdAsync(technicianId);
            var resources = _mapper
                .Map<IEnumerable<Appointment>, IEnumerable<AppointmentResource>>(appointments);
            return resources;
        }

        [AllowAnonymous]
        [SwaggerOperation(Tags = new[] { "appointments" })]
        [HttpPost]
        [ProducesResponseType(typeof(AppointmentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveAppointmentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var appointment = _mapper.Map<SaveAppointmentResource, Appointment>(resource);
            var result = await _appointmentService.SaveAsync(appointment);

            if (!result.Success)
                return BadRequest(result.Message);
            var appointmentResource = _mapper.Map<Appointment, AppointmentResource>(result.Resource);
            return Ok(appointmentResource);
        }

        [SwaggerOperation(Tags = new[] { "appointments" })]
        [HttpPut("{appointmentId}")]
        [ProducesResponseType(typeof(AppointmentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int appointmentId, [FromBody] SaveAppointmentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var appointment = _mapper.Map<SaveAppointmentResource, Appointment>(resource);
            var result = await _appointmentService.UpdateAsync(appointmentId, appointment);

            if (!result.Success)
                return BadRequest(result.Message);
            var appointmentResource = _mapper.Map<Appointment, AppointmentResource>(result.Resource);
            return Ok(appointmentResource);
        }

        [SwaggerOperation(Tags = new[] { "appointments" })]
        [HttpGet("{appointmentId}")]
        [ProducesResponseType(typeof(AppointmentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int appointmentId)
        {
            var result = await _appointmentService.GetByIdAsync(appointmentId);

            if (!result.Success)
                return BadRequest(result.Message);

            var appointmentResource = _mapper.Map<Appointment, AppointmentResource>(result.Resource);

            return Ok(appointmentResource);
        }

        [SwaggerOperation(Tags = new[] { "appointments" })]
        [HttpDelete("{appointmentId}")]
        [ProducesResponseType(typeof(AppointmentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int appointmentId)
        {
            var result = await _appointmentService.DeleteAsync(appointmentId);
            if (!result.Success)
                return BadRequest(result.Message);
            var appointmentResource = _mapper.Map<Appointment, AppointmentResource>(result.Resource);
            return Ok(appointmentResource);
        }
    }
}
