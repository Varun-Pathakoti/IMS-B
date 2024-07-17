using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSDomain.DTO
{
    public class UpdateProductDTO
    {
        public string ProductName { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [Range(0, 1000000, ErrorMessage = "Price cannot be negative.")]
        public float Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Threshold cannot be negative.")]
        public int Threshold { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Stock level cannot be negative.")]
        public int StockLevel { get; set; }
    }
}


