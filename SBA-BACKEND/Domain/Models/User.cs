﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBA_BACKEND.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public Profile Profile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Address Address { get; set; }
        public string UserType { get; set; }
        public string Token { get; set; }
        public DateTime RegisterDate { get; set; }

        //One to Many Relationship with FK
        // public int DistrictId { get; set; }
        // public District District { get; set; }



    }
}
