using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Customer {
    public class CreateCustomerDto {

        [Required]
        [MinLength(5, ErrorMessage = "Name must be 5 characters")]
        [MaxLength(100, ErrorMessage = "Name cannot be over 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Description must be 5 characters")]
        [MaxLength(1000, ErrorMessage = "Descriptin cannot be over 1000 characters")]
        public string? Description { get; set; } = string.Empty;
        
        public DateTime YearOfBirth { get; set; }
        public string? National { get; set; }
    }
}