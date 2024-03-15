using apimysqlrodrigomoro.Entities;
using Microsoft.EntityFrameworkCore;

namespace apimysqlrodrigomoro.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Product> Products { get; set; }
    }
}
