using AutoMapper;
using SBA_BACKEND.Domain.Models;
using SBA_BACKEND.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBA_BACKEND.Mapping
{
    public class ModelToResourceProfile : AutoMapper.Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Customer, CustomerResource>();
            CreateMap<Address, AddressResource>();
            CreateMap<Opinion, OpinionResource>();
            CreateMap<Report, ReportResource>();
            CreateMap<Specialty, SpecialtyResource>();
            CreateMap<Technician, TechnicianResource>();
            CreateMap<User, UserResource>();
        }
    }
}
