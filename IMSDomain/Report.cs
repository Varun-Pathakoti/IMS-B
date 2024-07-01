using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSDomain
{
    public class Report
    {
        public IEnumerable<Product> Product { get; set; }
        public IEnumerable<string> FastMovingProducts { get; set; }
        public IEnumerable<string> SlowMovingProducts { get; set; }
    }

}
