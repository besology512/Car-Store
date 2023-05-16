using System.ComponentModel.DataAnnotations;

namespace Car_Store.models
{
    public class product
    {
        [Required(ErrorMessage = "Please enter a name.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Category { get; set; }

        
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Description { get; set; }

        [Required]
        public int branchId { get; set; }

        [Required]
        [MaxLength(100)]
        public string brand { get; set; }

        [Required]
        public int price { get; set; }

        [Required]
        public int status { get; set; } // 1 avilibale 0 unavilibale 

        public string image { get; set; }

        [Required]
        public int quantity { get; set; }

        [Required]
        public int Id { get; set; }
    }

}
