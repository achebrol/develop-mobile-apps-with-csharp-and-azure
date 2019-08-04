using Microsoft.EntityFrameworkCore;

namespace Tailwinds.OData.Models
{
    public class TailwindsContext : DbContext
    {
        public TailwindsContext(DbContextOptions<TailwindsContext> options)
            : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AlbumConfiguration());
        }
    }
}
