using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMSBusinessLogic.MediatR.Queries;
using IMSDataAccess;
using IMSDomain;
using MediatR;

namespace IMSBusinessLogic.MediatR.Handlers
{
    public class GetByIdHandler : IRequestHandler<GetByIdQuery, Product>
    {
        private readonly IInventory _data;

        public GetByIdHandler(IInventory data)
        {
            _data = data;
        }
        public async Task<Product> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _data.getbyId(request.id);
            return product;
        }
    }
}
