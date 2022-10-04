namespace BookStore.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
        //quan hệ 1-n
        //1 cuốn sách thuộc về tác giả
        //1 tác giả có thể có nhiều sách
    }
}
