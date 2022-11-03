using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Customer.Customer.API.Resources;
using SBA_BACKEND.Technician.Technician.API.Resources;

namespace SBA_BACKEND.Report.Report.API.Resources
{
    public class ReportResource
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public TechnicianResource Technician { get; set; }
        public CustomerResource Customer { get; set; }
    }
}
