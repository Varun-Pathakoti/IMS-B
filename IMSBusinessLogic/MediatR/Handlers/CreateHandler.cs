using IMSBusinessLogic.MediatR.Commands;
using IMSDataAccess;
using IMSDomain;
using MediatR;

namespace IMSBusinessLogic.MediatR.Handlers
{
    public class CreateHandler : IRequestHandler<CreateCommand, Product>
    {
        private readonly IInventoryRepository _data;

        public CreateHandler(IInventoryRepository data)
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
