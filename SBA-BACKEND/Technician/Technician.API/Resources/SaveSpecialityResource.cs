using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SBA_BACKEND.Technician.Technician.API.Resources
{
    public class SaveSpecialtyResource
    {
        [Required]
        public string Name { get; set; }
    }
}
