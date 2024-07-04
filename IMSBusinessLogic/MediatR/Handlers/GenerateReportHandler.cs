using IMSBusinessLogic.MediatR.Queries;
using IMSDataAccess;
using IMSDomain;
using MediatR;

namespace IMSBusinessLogic.MediatR.Handlers
{
    public class GenerateReportHandler : IRequestHandler<GenerateReportQuery,string>
    {
        private readonly IInventory _data;

        public GenerateReportHandler(IInventory data)
        {
            _data = data;
        }

        async Task <string> IRequestHandler<GenerateReportQuery, string>.Handle(GenerateReportQuery request, CancellationToken cancellationToken)
        {
            var rep = await _data.GenerateReport();
            return rep;
        }
    }
}
