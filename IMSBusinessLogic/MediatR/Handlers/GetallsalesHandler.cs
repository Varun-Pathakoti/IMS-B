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
    public class GetAllSalesHandler : IRequestHandler<GetallSalesQuery, List<Sale>>
    {
        private readonly IInventoryRepository _data;

        public GetAllSalesHandler(IInventoryRepository data)
        {
            _data = data;
        }
        public async Task<List<Sale>> Handle(GetallSalesQuery request, CancellationToken cancellationToken)
        {
            var sales =await _data.GetAllSale();
            return sales;
        }
    }
}
