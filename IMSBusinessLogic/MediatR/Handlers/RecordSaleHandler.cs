using IMSBusinessLogic.MediatR.Queries;
using IMSDataAccess;
using IMSDomain;
using MediatR;

namespace IMSBusinessLogic.MediatR.Handlers
{
    public class RecordSaleHandler : IRequestHandler<RecordSaleQuery, List<Product>>
    {
        private readonly IInventory _data;

        public RecordSaleHandler(IInventory data)
        {
            _data = data;
        }
        public async Task<List<Product>> Handle(RecordSaleQuery request, CancellationToken cancellationToken)
        {
            var pro = await _data.RecordSales(request.sales);
            return pro;
        }
    }
}
