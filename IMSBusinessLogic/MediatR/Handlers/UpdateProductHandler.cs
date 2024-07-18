using IMSBusinessLogic.MediatR.Commands;
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
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly IInventoryRepository _data;
        public UpdateProductHandler(IInventoryRepository data)
        {
            _data = data;   
        }
        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var _product = await _data.UpdateProduct(request.Id, request.product);
            return _product;
        }
    }
}
