using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SBA_BACKEND.Report.Report.API.Resources
{
    public class SaveReportResource
    {
        [Required]
        public string Description { get; set; }
    }
}
