using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBA_BACKEND.Domain.Services.Communications;

namespace SBA_BACKEND.Report.Report.API.Services.Communications
{
    public class ReportResponse : BaseResponse<Report.Domain.AgreggatesModel.Report>
    {
        public ReportResponse(Report.Domain.AgreggatesModel.Report resource) : base(resource)
        {
        }

        public ReportResponse(string message) : base(message)
        {
        }
    }
}
