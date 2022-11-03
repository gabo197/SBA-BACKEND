using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBA_BACKEND.Technician.Technician.Domain.AgreggatesModel
{
    public class Specialty
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Many to Many reverse Relationship
        public List<TechnicianSpecialty> TechnicianSpecialties { get; set; }
    }
}
