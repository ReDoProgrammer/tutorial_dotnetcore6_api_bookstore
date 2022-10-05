namespace BookStore.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }

        public List<AuthorBook> AuthorBooks { get; set; }
    }
}
