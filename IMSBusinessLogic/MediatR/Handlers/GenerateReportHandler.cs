using IMSBusinessLogic.MediatR.Queries;
using IMSDataAccess;
using IMSDomain;
using MediatR;

namespace IMSBusinessLogic.MediatR.Handlers
{
    public class GenerateReportHandler : IRequestHandler<GenerateReportQuery,string>
    {
        private readonly IInventoryRepository _data;

        public GenerateReportHandler(IInventoryRepository data)
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
