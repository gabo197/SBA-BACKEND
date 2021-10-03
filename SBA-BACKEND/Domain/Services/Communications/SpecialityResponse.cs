using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Domain.Models;

namespace SBA_BACKEND.Domain.Services.Communications
{
	public class SpecialtyResponse : BaseResponse<Specialty>
	{
		public SpecialtyResponse(Specialty resource) : base(resource)
		{
		}

		public SpecialtyResponse(string message) : base(message)
		{
		}
	}
}
