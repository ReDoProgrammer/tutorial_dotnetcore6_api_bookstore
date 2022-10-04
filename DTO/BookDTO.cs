namespace BookStore.DTO
{
    public class BookDTO
    {
        public string Title { get; set; }
        public int Price { get; set; }
        public Guid AuthorId { get; set; }
    }
}
