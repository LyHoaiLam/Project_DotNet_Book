using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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