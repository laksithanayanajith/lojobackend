using lojobackend.Models;
using Microsoft.EntityFrameworkCore;

namespace lojobackend.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Item> items { get; set; }
        public DbSet<Image> images { get; set; }
        public DbSet<Color> colors { get; set; }
        public DbSet<lojobackend.Models.SelectedItem> SelectedItem { get; set; } = default!;
    }
}
