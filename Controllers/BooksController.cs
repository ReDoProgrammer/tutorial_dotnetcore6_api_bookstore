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
            return Ok(await _context.Books.ToListAsync());
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
                Price = requestBook.Price
               
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
