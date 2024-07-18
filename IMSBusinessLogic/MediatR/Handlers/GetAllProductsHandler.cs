using IMSBusinessLogic.MediatR.Queries;
using MediatR;
using IMSDataAccess;
using IMSDomain.Entities;

namespace IMSBusinessLogic.MediatR.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
    {
        private readonly IInventoryRepository _data;

        public GetAllProductsHandler(IInventoryRepository data)
        {
            _data = data;
        }
        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _data.GetAll();
            return products;
        }
    }
}
