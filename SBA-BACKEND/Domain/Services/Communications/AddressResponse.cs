using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Domain.Models;

namespace SBA_BACKEND.Domain.Services.Communications
{
	public class AddressResponse : BaseResponse<Address>
	{
		public AddressResponse(Address resource) : base(resource)
		{
		}

		public AddressResponse(string message) : base(message)
		{
		}
	}
}
