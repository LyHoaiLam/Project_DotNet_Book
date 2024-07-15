using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Book {
    public class CreateBookDto {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 characters")]
        [MaxLength(1000, ErrorMessage = "Title cannot be over 1000 characters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Description must be 5 characters")]
        [MaxLength(1000, ErrorMessage = "Description cannot be over 1000 characters")]
        public string? Description { get; set; } = string.Empty;

        [Required]
        [Range(1, 1000000)]
        public double Price { get; set; }

        [Required]
        [Range(1, 1000000)]
        public int Amount { get; set; }
    }
}