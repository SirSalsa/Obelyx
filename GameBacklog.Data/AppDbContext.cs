using GameBacklog.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameBacklog.Data
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Game>(entity =>
            {
                entity.ToTable("Games");

                entity.HasKey(g => g.Id);

                entity.Property(g => g.Title)
                      .IsRequired()
                      .HasMaxLength(200);

                // The rest (ImagePath, ReleaseYear, etc.) can be left alone,
                // EF will map them automatically since they are simple scalar props.
            });
        }
    }
}
