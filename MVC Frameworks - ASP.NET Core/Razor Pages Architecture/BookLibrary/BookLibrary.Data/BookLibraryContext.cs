namespace BookLibrary.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class BookLibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Borrower> Borrowers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder
                    .UseSqlServer("Data Source=.;Database=BookLibraryDb;Integrated Security=True");
            }

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Book>()
                .HasMany(book => book.Borrowers)
                .WithOne(borrower => borrower.Book)
                .HasForeignKey(bb => bb.BookId);

            builder.Entity<Borrower>()
                .HasMany(borrower => borrower.BorrowedBooks)
                .WithOne(book => book.Borrower)
                .HasForeignKey(bb => bb.BorrowerId);

            builder.Entity<BorrowersBooks>()
                .HasKey(bb => new { bb.BookId, bb.BorrowerId });

            base.OnModelCreating(builder);
        }
    }
}