using System.ComponentModel.DataAnnotations;

namespace api.models {
    public class Customer {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
        
        [Required]
        public DateTime YearOfBirth { get; set; }

        public string? National { get; set; }

    }
}