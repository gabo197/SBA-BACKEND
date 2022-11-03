using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Domain.Services.Communications;
using SBA_BACKEND.Report.Report.Domain.AgreggatesModel;

namespace SBA_BACKEND.Report.Report.API.Services.Communications
{
    public class OpinionResponse : BaseResponse<Opinion>
    {
        public OpinionResponse(Opinion resource) : base(resource)
        {
        }

        public OpinionResponse(string message) : base(message)
        {
        }
    }
}
