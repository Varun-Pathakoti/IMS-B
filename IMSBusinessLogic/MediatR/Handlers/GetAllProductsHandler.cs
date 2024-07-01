using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using IMSBusinessLogic.MediatR.Queries;
using MediatR;
using IMSDomain;
using IMSDataAccess;

namespace IMSBusinessLogic.MediatR.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
    {
        private readonly IInventory _data;

        public GetAllProductsHandler(IInventory data)
        {
            _data = data;
        }
        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _data.getAll();
            return products;
        }
    }
}
