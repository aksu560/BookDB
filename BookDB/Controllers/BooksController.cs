using BookDB.Data;
using BookDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookDB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : Controller
    {
        private readonly BookDBContext dbContext;

        public BooksController(BookDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] GetBooksRequest getBooksRequest)
        {
            var query = dbContext.Books.AsQueryable();
            if (!string.IsNullOrEmpty(getBooksRequest.Author))
            {
                query = query.Where(book => book.Author == getBooksRequest.Author);
            }
            if (getBooksRequest.Year != null)
            {
                query = query.Where(book => book.Year == getBooksRequest.Year);
            }
            if (!string.IsNullOrEmpty(getBooksRequest.Publisher))
            {
                query = query.Where(book => book.Publisher == getBooksRequest.Publisher);
            }
            var books = await query.ToListAsync();
            return Ok(books);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetBook([FromRoute] int id)
        {
            var book = await dbContext.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookRequest addBookRequest)
        {
            Book book = new Book()
            {
                Title = addBookRequest.Title,
                Author = addBookRequest.Author,
                Year = addBookRequest.Year,
                Publisher = addBookRequest.Publisher,
                Description = addBookRequest.Description
            };

            if (book.Title == string.Empty || book.Author == string.Empty || book.Publisher == string.Empty)
            {
                return BadRequest();
            }

            var duplicateBook = await dbContext.Books.FirstOrDefaultAsync(b => b.Title == book.Title && b.Author == book.Author);

            // Slightly dirty, could probably be done better.
            if (duplicateBook != null)
            {
                if (book.Publisher == null)
                {
                    return BadRequest();
                }
                if (book.Publisher == duplicateBook.Publisher)
                {
                    return BadRequest();
                }
            }

            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();
            return Ok(book.Id);
        }


        /*
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, UpdateBookRequest updateBookRequest)
        {
            var book = await dbContext.Books.FindAsync(id);
            
            if (book == null)
            {
                return NotFound();
            }

            book.Title = updateBookRequest.Title;
            book.Author = updateBookRequest.Author;
            book.Year = updateBookRequest.Year;
            book.Publisher = updateBookRequest.Publisher;
            book.Description = updateBookRequest.Description;

            await dbContext.SaveChangesAsync();

            return Ok(book);
        }
        */

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteContact([FromRoute] int id)
        {
            var book = await dbContext.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            dbContext.Books.Remove(book);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

    }
}
