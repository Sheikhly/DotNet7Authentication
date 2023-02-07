 using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScaffoldDotNet7.Models;

namespace ScaffoldDotNet7.Data
{
    //Third Step: Adding Application User Inside The " <  > "
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Movie> Movies { get; set; }
    }
}