using Microsoft.AspNetCore.Identity;

namespace api.models {
    public class AppUser : IdentityUser {
        public int Risk {get; set;}
        public List<AuthorList> authorLists { get; set; } = new List<AuthorList>();
        
    }
}