namespace BookStore.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public List<AuthorBook> AuthorBooks  { get; set; }
    }
}
