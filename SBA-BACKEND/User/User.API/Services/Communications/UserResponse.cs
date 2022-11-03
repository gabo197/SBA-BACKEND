using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Domain.Services.Communications;

namespace SBA_BACKEND.User.User.API.Services.Communications
{
    public class UserResponse : BaseResponse<User.Domain.AgreggatesModel.User>
    {
        public UserResponse(User.Domain.AgreggatesModel.User resource) : base(resource)
        {
        }

        public UserResponse(string message) : base(message)
        {
        }
    }
}
