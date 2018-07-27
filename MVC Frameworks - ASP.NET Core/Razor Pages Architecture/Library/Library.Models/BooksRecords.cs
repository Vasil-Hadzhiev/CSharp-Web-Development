namespace Library.Models
{
    public class BooksRecords
    {
        public int RecordId { get; set; }
        public Record Record { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
