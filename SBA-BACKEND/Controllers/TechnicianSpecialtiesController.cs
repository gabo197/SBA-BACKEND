using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SBA_BACKEND.Domain.Models;
using SBA_BACKEND.Domain.Services;
using SBA_BACKEND.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBA_BACKEND.Controllers
{
    [Authorize]
    [Route("api/technician/{userId}/specialties")]
    [ApiController]
    public class TechnicianSpecialtiesController : ControllerBase
    {
        private readonly ISpecialtyService specialtyService;
        private readonly ITechnicianSpecialtyService technicianSpecialtyService;
        private readonly IMapper mapper;

        public TechnicianSpecialtiesController(ISpecialtyService specialtyService, ITechnicianSpecialtyService technicianSpecialtyService, IMapper mapper)
        {
            this.specialtyService = specialtyService;
            this.technicianSpecialtyService = technicianSpecialtyService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SpecialtyResource>> GetAllByTechnicianIdAsync(int userId)
        {
            var specialties = await specialtyService.ListByTechnicianIdAsync(userId);
            var resources = mapper.Map<IEnumerable<Specialty>, IEnumerable<SpecialtyResource>>(specialties);
            return resources;
        }

        [HttpPost("{specialtyId}")]
        public async Task<IActionResult> AssignTechnicianSpecialty(int userId, int specialtyId)
        {
            var result = await technicianSpecialtyService.AssignTechnicianSpecialtyAsync(userId, specialtyId);
            if (!result.Success)
                return BadRequest(result.Message);

            var specialtyResource = mapper.Map<Specialty, SpecialtyResource>(result.Resource.Specialty);
            return Ok(specialtyResource);
        }

        [HttpDelete("{specialtyId}")]
        public async Task<IActionResult> UnassignTechnicianSpecialty(int userId, int specialtyId)
        {
            var result = await technicianSpecialtyService.UnassignTechnicianSpecialtyAsync(userId, specialtyId);
            if (!result.Success)
                return BadRequest(result.Message);

            var specialtyResource = mapper.Map<Specialty, SpecialtyResource>(result.Resource.Specialty);
            return Ok(specialtyResource);
        }

    }
}
