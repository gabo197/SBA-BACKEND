using AutoMapper;
using SBA_BACKEND.Customer.Customer.API.Resources;
using SBA_BACKEND.Customer.Customer.Domain.AgreggatesModel;
using SBA_BACKEND.Report.Report.API.Resources;
using SBA_BACKEND.Report.Report.Domain.AgreggatesModel;
using SBA_BACKEND.Technician.Technician.API.Resources;
using SBA_BACKEND.Technician.Technician.Domain.AgreggatesModel;
using SBA_BACKEND.User.User.API.Resources;
using SBA_BACKEND.User.User.Domain.AgreggatesModel;
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
            CreateMap<Customer.Customer.Domain.AgreggatesModel.Customer, CustomerResource>();
            CreateMap<Address, AddressResource>();
            CreateMap<Appointment, AppointmentResource>();
            CreateMap<PaymentMethod, PaymentMethodResource>();
            CreateMap<Opinion, OpinionResource>();
            CreateMap<Report.Report.Domain.AgreggatesModel.Report, ReportResource>();
            CreateMap<Specialty, SpecialtyResource>();
            CreateMap<Technician.Technician.Domain.AgreggatesModel.Technician, TechnicianResource>();
            CreateMap<User.User.Domain.AgreggatesModel.User, UserResource>();
        }
    }
}
