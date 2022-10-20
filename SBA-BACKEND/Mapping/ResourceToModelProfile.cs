using AutoMapper;
using SBA_BACKEND.Domain.Models;
using SBA_BACKEND.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBA_BACKEND.Mapping
{
    public class ResourceToModelProfile : AutoMapper.Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveCustomerResource, Customer>();
            CreateMap<SaveAddressResource, Address>();
            CreateMap<SaveAppointmentResource, Appointment>();
            CreateMap<SaveOpinionResource, Opinion>();
            CreateMap<SaveReportResource, Report>();
            CreateMap<SaveSpecialtyResource, Specialty>();
            CreateMap<SaveTechnicianResource, Technician>();
            CreateMap<SaveUserResource, User>();
        }
    }
}
