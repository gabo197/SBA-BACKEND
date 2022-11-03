using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBA_BACKEND.Technician.Technician.Domain.AgreggatesModel
{
    public class TechnicianSpecialty
    {
        //Many to Many Relationship
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }

        public int TechnicianId { get; set; }
        public Technician Technician { get; set; }
    }
}
