namespace Library.Web.Models.ViewModels
{
    public class AllBooksViewModel
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public int AuthorId { get; set; }

        public string Author { get; set; }

        public string Status { get; set; }
    }
}