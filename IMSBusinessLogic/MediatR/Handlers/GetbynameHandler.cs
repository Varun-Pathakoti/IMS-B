using IMSBusinessLogic.MediatR.Queries;
using IMSDataAccess;
using IMSDomain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSBusinessLogic.MediatR.Handlers
{
    public class GetbynameHandler : IRequestHandler<GetByNameQuery, List<Product>>
    {
        private readonly IInventoryRepository data;
        public GetbynameHandler(IInventoryRepository data)
        {
            this.data = data;
        }
        public async Task<List<Product>> Handle(GetByNameQuery request, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            var product = await data.GetByName(request.name);
            return product;
        }
    }
}
