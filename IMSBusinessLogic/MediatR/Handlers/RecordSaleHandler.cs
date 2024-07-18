using IMSBusinessLogic.MediatR.Queries;
using IMSDataAccess;
using IMSDomain;
using MediatR;

namespace IMSBusinessLogic.MediatR.Handlers
{
    public class RecordSaleHandler : IRequestHandler<RecordSaleQuery, List<int>>
    {
        private readonly IInventoryRepository _data;

        public RecordSaleHandler(IInventoryRepository data)
        {
            _data = data;
        }

        public async Task<List<int>> Handle(RecordSaleQuery request, CancellationToken cancellationToken)
        {
            var product = await _data.RecordSales(request.sales);
            return product;
        }
    }
}
