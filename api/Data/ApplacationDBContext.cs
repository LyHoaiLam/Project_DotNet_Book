using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Data {
    public class ApplacationDBContext : DbContext {
        public ApplacationDBContext(DbContextOptions<ApplacationDBContext> options) : base(options) {}
            public DbSet<Book> Book { get; set; }
            public DbSet<Customer> Customer { get; set; }
    }
}