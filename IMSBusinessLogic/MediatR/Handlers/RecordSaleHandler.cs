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
    public class RecordSaleHandler : IRequestHandler<RecordSaleQuery, Product>
    {
        private readonly IInventory _data;

        public RecordSaleHandler(IInventory data)
        {
            _data = data;
        }
        public async Task<Product> Handle(RecordSaleQuery request, CancellationToken cancellationToken)
        {
            var pro = await _data.RecordSale(request.productid, request.quantity);
            return pro;
        }
    }
}
