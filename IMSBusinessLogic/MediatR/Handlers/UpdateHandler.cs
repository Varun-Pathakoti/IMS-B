using IMSBusinessLogic.MediatR.Commands;
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
    public class UpdateHandler : IRequestHandler<UpdateCommand, Product>
    {
        private readonly IInventory _data;

        public UpdateHandler(IInventory data)
        {
            _data = data;
        }
        public async Task<Product> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var pro=await _data.update(request.Id,request.stock);
            return pro;
        }
    }
}
