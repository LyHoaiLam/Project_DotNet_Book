using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.models {
    [Table("Customer")]
    public class Customer {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public string? Description { get; set; }
        
        [Required]
        public DateTime YearOfBirth { get; set; }

        public string? National { get; set; }

        public List<Book>? Books { get; set; } = new List<Book>();
        public List<AuthorList> authorLists { get; set; } = new List<AuthorList>();

    }
}