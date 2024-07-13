using api.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data {
    public class ApplacationDBContext : IdentityDbContext<AppUser> {
        public ApplacationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) {}
            public DbSet<Book> Book { get; set; }
            public DbSet<Customer> Customer { get; set; }
            public DbSet<AuthorList> AuthorLists { get; set; }

            protected override void OnModelCreating(ModelBuilder builder) {
                base.OnModelCreating(builder);
                //Thiết lập khóa ngoại
                builder.Entity<AuthorList>(x => x.HasKey(p => new { p.AppUserId, p.CustomerId}));
                //Kết nối vs Table
                builder.Entity<AuthorList>().HasOne(u => u.AppUser).WithMany(u => u.authorLists).HasForeignKey(p => p.AppUserId);

                builder.Entity<AuthorList>().HasOne(u => u.Customer).WithMany(u => u.authorLists).HasForeignKey(p => p.CustomerId);

                List<IdentityRole> roles = new List<IdentityRole> {
                    new IdentityRole {
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },
                    new IdentityRole {
                        Name = "User",
                        NormalizedName = "USER"
                    },

                };
                    builder.Entity<IdentityRole>().HasData(roles);
            }
    }
}