using BookStore.DTO;
using BookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private DataContext _context;

        

        //contructor
        //hàm khởi tạo or hàm dựng
        public BooksController(DataContext _context)
        {
            this._context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            //thay vì chỉ hiển thị id của tác giả,
            //chúng ta cần hiển thị họ tên của tác giả
            //bằng việc sử dụng join trong linq
            var result = await (
                    from b in _context.Books //từ sách
                    join au in _context.Authors//join sang tác giả
                    on b.AuthorId equals au.Id // điều kiện join
                    select new//khởi tạo 1 đối tượng từ 2 tập hợp trên, lọc ra các thông tin cần lấy
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Price = b.Price,
                        Author = string.Format("{0} {1}",au.Firstname,au.Lastname)
                    }
                ).ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var book = await _context.Books.FindAsync(id);
            if(book == null)
            {
                return NotFound("Book not found");
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookDTO requestBook)
        {
            //define a new book object 
            var book = new Book()
            {
                Id = new Guid(),
                Title = requestBook.Title,
                Price = requestBook.Price,
                AuthorId = requestBook.AuthorId
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return Ok(book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id,BookDTO requestBook)
        {
            var book = await _context.Books.FindAsync(id);
            
            if (book == null) return NotFound("Book not found");

            book.Title = requestBook.Title;
            book.Price = requestBook.Price;
            book.AuthorId = requestBook.AuthorId;

            await _context.SaveChangesAsync();
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound("Book not found");

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return Ok("The book has been deleted!");
        }
    }
}
