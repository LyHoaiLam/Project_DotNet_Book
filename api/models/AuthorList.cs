using System.ComponentModel.DataAnnotations.Schema;

namespace api.models {
    [Table("AuthorList")]
    public class AuthorList {
        public string AppUserId { get; set; }
        public int CustomerId { get; set; }
        public AppUser AppUser { get; set; }

        //Dành cho nhà phát triển
        public Customer Customer { get; set; }
        
    }
}