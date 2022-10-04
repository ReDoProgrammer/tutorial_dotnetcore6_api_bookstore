using BookStore.DTO;
using BookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly DataContext context;

        public AuthorsController(DataContext _context)
        {
            context = _context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            return Ok(await context.Authors.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorDTO requestAuthor)
        {
            var author = new Author()
            {
                Id = new Guid(),
                Firstname = requestAuthor.Firstname,
                Lastname = requestAuthor.Lastname,
                Phone = requestAuthor.Phone
            };

            context.Authors.Add(author);
            await context.SaveChangesAsync();
            return Ok(author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id,AuthorDTO requestAuthor)
        {
            var author = await context.Authors.FindAsync(id);
            if (author == null) return NotFound("Không tìm thấy tác giả!");
            author.Firstname = requestAuthor.Firstname;
            author.Lastname = requestAuthor.Lastname;
            author.Phone = requestAuthor.Phone;

            await context.SaveChangesAsync();
            return Ok(author);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var author = await context.Authors.FindAsync(id);
            if (author == null) return NotFound("Không tìm thấy tác giả!");

            //kiểm tra xem tác giả này có sách nào hay không
            //nếu có thì sẽ không cho xóa
            var count = await context.Books.CountAsync(x=>x.AuthorId == id);
            if (count > 0) return BadRequest("Không thể xóa tác giả này khi có sách do tác giả này viết!");

            //ngược lại, count = 0
            context.Authors.Remove(author);
            await context.SaveChangesAsync();
            return Ok("Xóa tác giả thành công!");
        }
    }
}
