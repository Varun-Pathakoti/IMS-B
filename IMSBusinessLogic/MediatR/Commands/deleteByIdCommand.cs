using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSBusinessLogic.MediatR.Commands
{
    public class DeleteByIdCommand : IRequest
    {
        public int Id { get; set; }
        public DeleteByIdCommand(int id)
        {
            this.Id = id;
        }
    }
}
