using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSDomain
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }        
        public int ProductId { get; set; }
        public String ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
