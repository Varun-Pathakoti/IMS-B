using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IMSDomain;
using MediatR;


namespace IMSBusinessLogic.MediatR.Queries
{
    public class GetAllProductsQuery:IRequest<List<Product>>
    {
    }
}
