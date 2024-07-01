using IMSBusinessLogic.MediatR.Queries;
using IMSDataAccess;
using IMSDomain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSBusinessLogic.MediatR.Handlers
{
    public class GenerateReportHandler : IRequestHandler<GenerateReportQuery,Report>
    {
        private readonly IInventory _data;

        public GenerateReportHandler(IInventory data)
        {
            _data = data;
        }

        async Task <Report> IRequestHandler<GenerateReportQuery, Report>.Handle(GenerateReportQuery request, CancellationToken cancellationToken)
        {
            var rep = await _data.GenerateReport();
            return rep;
        }
    }
}
