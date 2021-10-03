﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBA_BACKEND.Domain.Models
{
    public class Technician : Profile
    {
        //Many to Many reverse Relationship
        public List<TechnicianSpecialty> TechnicianSpecialties { get; set; }

        //One to Many Reverse Relationship
        public IList<Opinion> Opinions { get; set; } = new List<Opinion>();

        //One to Many Reverse Relationship
        public IList<Report> Reports { get; set; } = new List<Report>();
    }
}
