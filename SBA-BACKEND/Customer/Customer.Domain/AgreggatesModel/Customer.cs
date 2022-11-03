﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Report.Report.Domain.AgreggatesModel;
using SBA_BACKEND.User.User.Domain.AgreggatesModel;

namespace SBA_BACKEND.Customer.Customer.Domain.AgreggatesModel
{
    public class Customer : Profile
    {

        //One to Many Reverse Relationship
        public IList<Opinion> Opinions { get; set; } = new List<Opinion>();
        //One to Many Reverse Relationship
        public IList<Report.Report.Domain.AgreggatesModel.Report> Reports { get; set; } = new List<Report.Report.Domain.AgreggatesModel.Report>();
        public IList<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
