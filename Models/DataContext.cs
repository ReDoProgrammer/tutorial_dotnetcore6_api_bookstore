using Microsoft.EntityFrameworkCore;

namespace BookStore.Models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options){}
       
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
        //vì entity authorbook gồm 2 id: bookid và authorid
        //nên chúng ta cần chỉ rõ khóa chính của entity này gồm 2 prop trên
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AuthorBook>()
                .HasKey(k=>new { k.AuthorId,k.BookId});
        }
    }
}
