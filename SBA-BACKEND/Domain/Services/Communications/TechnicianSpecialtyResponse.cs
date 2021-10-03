using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Domain.Models;

namespace SBA_BACKEND.Domain.Services.Communications
{
	public class TechnicianSpecialtyResponse : BaseResponse<TechnicianSpecialty>
	{
		public TechnicianSpecialtyResponse(TechnicianSpecialty resource) : base(resource)
		{
		}

		public TechnicianSpecialtyResponse(string message) : base(message)
		{
		}
	}
}
