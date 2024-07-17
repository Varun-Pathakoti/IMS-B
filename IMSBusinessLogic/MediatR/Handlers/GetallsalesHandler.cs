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
    public class GetallsalesHandler : IRequestHandler<GetallsalesQuery, List<Sale>>
    {
        private readonly IInventoryRepository _data;

        public GetallsalesHandler(IInventoryRepository data)
        {
            _data = data;
        }
        public async Task<List<Sale>> Handle(GetallsalesQuery request, CancellationToken cancellationToken)
        {
            var sales =await _data.getAllSale();
            return sales;
        }
    }
}
