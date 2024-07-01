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
    public class CreateHandler : IRequestHandler<CreateCommand, Product>
    {
        private readonly IInventory _data;

        public CreateHandler(IInventory data)
        {
            _data = data;
        }
        public async Task<Product> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                ProductName = request.ProductName,
                Description = request.Description,
                Price = request.Price,             
                Threshold = request.Threshold,
                StockLevel = request.StockLevel,
            };

            var createdProduct = await _data.create(product);

            return createdProduct;
        }
    }
}
