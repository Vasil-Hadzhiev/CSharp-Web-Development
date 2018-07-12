namespace FDMC.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class FdmcContext : DbContext
    {
        public DbSet<Cat> Cats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder
                    .UseSqlServer("Server=.;Database=FDMC_Db;Integrated Security=true;");
            }

            base.OnConfiguring(builder);
        }
    }
}