using BookStore.DTO;
using BookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorBooksController : ControllerBase
    {
        private readonly DataContext context;

        public AuthorBooksController(DataContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthorBooks()
        {
            var result = await (
                from ab in context.AuthorBooks
                join a in context.Authors //join tu authorbook qua author
                on ab.AuthorId equals a.Id //điều kiện join
                join b in context.Books //join authorbook tới book
                on ab.BookId equals b.Id //điều kiện join
                select new//khởi tạo 1 đối tượng để lấy các thông tin tường minh hơn cho người dùng
                {
                    BookId = b.Id,
                    Title = b.Title,
                    Price = b.Price,
                    Author = string.Format("{0} {1}",a.Firstname,a.Lastname)
                }
               ).ToListAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorBookDTO requestAB) 
        {
            var ab = new AuthorBook()
            {
                AuthorId = requestAB.AuthorId,
                BookId = requestAB.BookId
            };

            context.AuthorBooks.Add(ab);

            await context.SaveChangesAsync();

            return Ok("Thiết lập thông tin sách và tác giả thành công!");

        }
    }
}
