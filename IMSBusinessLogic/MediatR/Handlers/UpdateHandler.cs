using IMSBusinessLogic.MediatR.Commands;
using IMSDataAccess;
using IMSDomain;
using MediatR;

namespace IMSBusinessLogic.MediatR.Handlers
{
    public class UpdateHandler : IRequestHandler<UpdateCommand, Product>
    {
        private readonly IInventoryRepository _data;

        public UpdateHandler(IInventoryRepository data)
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
