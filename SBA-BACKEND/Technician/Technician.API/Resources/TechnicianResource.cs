﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Technician.Technician.Domain.AgreggatesModel;
using SBA_BACKEND.User.User.API.Resources;

namespace SBA_BACKEND.Technician.Technician.API.Resources
{
    public class TechnicianResource : ProfileResource
    {
        public List<TechnicianSpecialtyResource> TechnicianSpecialties { get; set; }
    }
}
