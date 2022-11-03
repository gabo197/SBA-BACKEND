using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SBA_BACKEND.User.User.API.Resources
{
    public class SaveAddressResource
    {
        [Required]
        public string Region { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        public string FullAddress { get; set; }
    }
}
