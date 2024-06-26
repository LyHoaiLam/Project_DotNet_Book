using System.ComponentModel.DataAnnotations;

namespace api.models {
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

        public int? CustomerId { get; set; }

    }
}