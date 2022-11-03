using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Domain.Services.Communications;

namespace SBA_BACKEND.Customer.Customer.API.Services.Communications
{
    public class CustomerResponse : BaseResponse<Customer.Domain.AgreggatesModel.Customer>
    {
        public CustomerResponse(Customer.Domain.AgreggatesModel.Customer resource) : base(resource)
        {
        }

        public CustomerResponse(string message) : base(message)
        {
        }
    }
}
