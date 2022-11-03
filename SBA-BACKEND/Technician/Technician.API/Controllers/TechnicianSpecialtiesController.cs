﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBA_BACKEND.Technician.Technician.API.Resources;
using SBA_BACKEND.Technician.Technician.API.Services;
using SBA_BACKEND.Technician.Technician.Domain.AgreggatesModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBA_BACKEND.Technician.Technician.API.Controllers
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
        [ProducesResponseType(typeof(SpecialtyResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> AssignTechnicianSpecialty(int userId, int specialtyId)
        {
            var result = await technicianSpecialtyService.AssignTechnicianSpecialtyAsync(userId, specialtyId);
            if (!result.Success)
                return BadRequest(result.Message);

            var specialtyResource = mapper.Map<Specialty, SpecialtyResource>(result.Resource.Specialty);
            return Ok(specialtyResource);
        }

        [HttpDelete("{specialtyId}")]
        [ProducesResponseType(typeof(SpecialtyResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
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
