using IMSBusinessLogic.MediatR.Commands;
using IMSDataAccess;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSBusinessLogic.MediatR.Handlers
{
    public class deleteByIdHandler : IRequestHandler<deleteByIdCommand>
    {
        private readonly IInventoryRepository data;
        public deleteByIdHandler(IInventoryRepository data)
        {
            this.data = data;
        }

        public async Task<Unit> Handle(deleteByIdCommand request, CancellationToken cancellationToken)
        {
            await data.deleteById(request.id);
            return Unit.Value;
        }


        //public async Task<Task> Handle(deleteByIdCommand request, CancellationToken cancellationToken)
        //{
        //    await data.deleteById(request.id);
        //    ;
        //}
    }
}
