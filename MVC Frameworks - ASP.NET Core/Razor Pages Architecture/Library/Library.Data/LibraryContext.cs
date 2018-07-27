namespace Library.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;

    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Borrower> Borrowers { get; set; }

        public DbSet<BooksRecords> BookRecords { get; set; }

        public DbSet<BorrowersBooks> BorrowersBooks { get; set; }

        public DbSet<BorrowersMovies> BorrowersMovies { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Director> Directors { get; set; }

        public DbSet<MoviesRecords> MovieRecords { get; set; }

        public DbSet<Record> Records { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder
                    .UseSqlServer("Data Source=.;Database=LibraryDb;Integrated Security=True");
            }

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Book>()
                .HasMany(book => book.Borrowers)
                .WithOne(borrower => borrower.Book)
                .HasForeignKey(bb => bb.BookId);

            builder
                .Entity<Borrower>()
                .HasMany(borrower => borrower.BorrowedBooks)
                .WithOne(book => book.Borrower)
                .HasForeignKey(bb => bb.BorrowerId);

            builder
                .Entity<BorrowersBooks>()
                .HasKey(bb => new { bb.BookId, bb.BorrowerId });

            builder
                .Entity<Book>()
                .Property(b => b.Status)
                .HasDefaultValue("At home");

            builder
               .Entity<Movie>()
               .Property(m => m.Status)
               .HasDefaultValue("At home");

            builder
                .Entity<Movie>()
                .HasMany(m => m.Borrowers)
                .WithOne(bm => bm.Movie)
                .HasForeignKey(bm => bm.MovieId);

            builder
                .Entity<Borrower>()
                .HasMany(b => b.BorrowedMovies)
                .WithOne(bm => bm.Borrower)
                .HasForeignKey(bm => bm.BorrowerId);

            builder
                .Entity<BorrowersMovies>()
                .HasKey(bm => new { bm.MovieId, bm.BorrowerId });

            builder
                .Entity<Record>()
                .HasMany(r => r.BookRecords)
                .WithOne(br => br.Record)
                .HasForeignKey(br => br.RecordId);

            builder
                .Entity<Book>()
                .HasMany(b => b.Records)
                .WithOne(br => br.Book)
                .HasForeignKey(br => br.BookId);

            builder
                .Entity<BooksRecords>()
                .HasKey(br => new { br.RecordId, br.BookId });

            builder
                .Entity<Record>()
                .HasMany(r => r.MovieRecords)
                .WithOne(mr => mr.Record)
                .HasForeignKey(mr => mr.RecordId);

            builder
                .Entity<Movie>()
                .HasMany(m => m.Records)
                .WithOne(mr => mr.Movie)
                .HasForeignKey(mr => mr.MovieId);

            builder
                .Entity<MoviesRecords>()
                .HasKey(mr => new { mr.RecordId, mr.MovieId });

            builder
                .Entity<Record>()
                .Property(r => r.StartDate)
                .HasDefaultValue(DateTime.Today);

            base.OnModelCreating(builder);
        }
    }
}