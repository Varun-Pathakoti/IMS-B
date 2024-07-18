using IMSDomain.DTO;
using IMSDomain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSBusinessLogic.MediatR.Commands
{
    public class UpdateProductCommand : IRequest<Product>
    {
        public UpdateProductDTO product { get; set; }
        public int Id {  get; set; }
        public UpdateProductCommand(int id, UpdateProductDTO product)
        {
            this.Id = id;
            this.product = product; 
        }
    }
}
