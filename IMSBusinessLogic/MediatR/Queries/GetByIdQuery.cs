using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMSDomain;
using MediatR;

namespace IMSBusinessLogic.MediatR.Queries
{
    public class GetByIdQuery:IRequest<Product>
    {
        //private readonly int id;
       public  int id { get; set; }

        public GetByIdQuery(int id) {
            this.id = id;
        }
    }
}
