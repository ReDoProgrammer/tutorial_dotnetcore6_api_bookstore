namespace BookStore.DTO
{
    public class AuthorBookDTO
    {
        public Guid AuthorId { get; set; } = Guid.Empty;
        public Guid BookId { get; set; } = Guid.Empty;
    }
}
