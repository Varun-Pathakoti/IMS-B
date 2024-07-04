using System.ComponentModel.DataAnnotations;

namespace IMSDomain
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        public string ProductName { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required,Range(0,1000000)]
        public float Price { get; set; }
        [Required]
        public int Threshold { get; set; }
        [Required]
        public int StockLevel { get; set; }
    }
}
