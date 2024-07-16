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
    public class GetbynameHandler : IRequestHandler<GetbynameQuery, Product>
    {
        private readonly IInventoryRepository data;
        public GetbynameHandler(IInventoryRepository data)
        {
            this.data = data;
        }
        public async Task<Product> Handle(GetbynameQuery request, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            var product = await data.getByName(request.name);
            return product;
        }
    }
}
