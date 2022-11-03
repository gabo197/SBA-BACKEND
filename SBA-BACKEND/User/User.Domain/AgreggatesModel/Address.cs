using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBA_BACKEND.User.User.Domain.AgreggatesModel
{
    public class Address
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string Region { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string FullAddress { get; set; }
    }
}
