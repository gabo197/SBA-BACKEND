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
    public class ResourceToModelProfile : AutoMapper.Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveCustomerResource, Customer.Customer.Domain.AgreggatesModel.Customer>();
            CreateMap<SaveAddressResource, Address>();
            CreateMap<SaveAppointmentResource, Appointment>();
            CreateMap<SaveOpinionResource, Opinion>();
            CreateMap<SaveReportResource, Report.Report.Domain.AgreggatesModel.Report>();
            CreateMap<SaveSpecialtyResource, Specialty>();
            CreateMap<SaveTechnicianResource, Technician.Technician.Domain.AgreggatesModel.Technician>();
            CreateMap<SaveUserResource, User.User.Domain.AgreggatesModel.User>();
        }
    }
}
