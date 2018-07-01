namespace Chushka.Data
{
    using Chushka.Models;
    using Microsoft.EntityFrameworkCore;

    public class ChushkaContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder
                    .UseSqlServer("Server=.;Database=ChushkaDbExam_Yulaw;Integrated Security=true;");
            }

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}