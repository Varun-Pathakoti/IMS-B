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
        [Required,Range(0,1000000, ErrorMessage ="Price cannot be negative.")]
        public float Price { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Threshold cannot be negative.")]
        public int Threshold { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock level cannot be negative.")]
        public int StockLevel { get; set; }
    }
}
