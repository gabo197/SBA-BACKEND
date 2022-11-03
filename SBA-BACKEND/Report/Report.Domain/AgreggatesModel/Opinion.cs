using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBA_BACKEND.Report.Report.Domain.AgreggatesModel
{
    public class Opinion
    {
        public int Id { get; set; }
        public int Stars { get; set; }
        public string Description { get; set; }

        //One to Many Relationship
        public int CustomerId { get; set; }
        public Customer.Customer.Domain.AgreggatesModel.Customer Customer { get; set; }

        //One to Many Relationship
        public int TechnicianId { get; set; }
        public Technician.Technician.Domain.AgreggatesModel.Technician Technician { get; set; }


    }
}
