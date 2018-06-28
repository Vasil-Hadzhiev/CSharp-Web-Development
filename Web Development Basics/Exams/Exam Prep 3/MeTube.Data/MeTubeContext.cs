namespace MeTube.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class MeTubeContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Tube> Tubes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder
                    .UseSqlServer("Server=.;Database=MeTubeDb;Integrated Security=true;");
            }

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<User>()
                .HasIndex(user => user.Username)
                .IsUnique();

            builder
                .Entity<User>()
                .HasIndex(user => user.Email)
                .IsUnique();

            base.OnModelCreating(builder);
        }
    }
}