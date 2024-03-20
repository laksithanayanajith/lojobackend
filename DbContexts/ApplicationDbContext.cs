using lojobackend.Models;
using Microsoft.EntityFrameworkCore;

namespace lojobackend.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Item> items { get; set; } = default!;
        public DbSet<Image> images { get; set; } = default!;
        public DbSet<Color> colors { get; set; } = default!;
        public DbSet<SelectedItem> SelectedItem { get; set; } = default!;
        public DbSet<Size> Size { get; set; } = default!;
    }
}
