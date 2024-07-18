using IMSDomain.Entities;
using MediatR;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSBusinessLogic.MediatR.Queries
{
    public class GetByNameQuery:IRequest<Product>
    {
        public string name {  get; set; }
        public GetByNameQuery(string name) { 
            this.name = name;
        }
    }
}
