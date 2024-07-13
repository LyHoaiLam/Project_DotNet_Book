using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.models {
    [Table("Books")]
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }
        
        [Required]
        public double Price { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public int? CustomerId { get; set; }

    }
}