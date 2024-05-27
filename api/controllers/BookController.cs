using api.Data;
using api.Dtos.Book;
using api.interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.controllers {
    [Route("api/Book")]
    [ApiController]
    public class BookController : ControllerBase {

        private readonly ApplacationDBContext _context;
        private readonly IBookRepository _bookRepo;

        public BookController(ApplacationDBContext context, IBookRepository bookRepo) {
            _context = context;
            _bookRepo = bookRepo;
        }
        
        // [HttpGet]
        //  public async Task<IActionResult> GetAll() {
        //    var books = _context.Book.ToList();

        //    if(books == null) {
        //         return BadRequest("adsadsadsadsad");
        //    }
        //    return Ok(books);
        // }

        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetById(int id) {
        //     var books = _context.Book.Find(id);

        //     if(books == null) {
        //         return BadRequest("Not Found Book ID THIS");
        //    }
        //    return Ok(books);
        // }

        // [HttpPost]
        // public async Task<IActionResult> Create([FromBody] Book book) {
        //     if (book == null) {
        //         return BadRequest("Invalid book data 3999.");
        //     }
        //     try {
        //         _context.Book.Add(book);
        //         await _context.SaveChangesAsync();
        //         return Ok(book);
        //     }
        //     catch (Exception ex) {
        //         return StatusCode(500, $"Internal server error: {ex.Message}");
        //     }
        // }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> Update(int id, [FromBody] Book book) {
        //     if (book == null ) {
        //         return BadRequest("Book data is invalid or IDs do not match.");
        //     }
        //     var bookUpdate = _context.Book.Find(id);
        //     if (bookUpdate == null) {
        //         return NotFound("Book not found.");
        //     }
        //     try {
        //         bookUpdate.Title = book.Title;
        //         bookUpdate.Description = book.Description;
        //         bookUpdate.Price = book.Price;
        //         bookUpdate.Amount = book.Amount;
        //         _context.Book.Update(bookUpdate);
        //         await _context.SaveChangesAsync();
        //         return Ok(bookUpdate);
        //     }
        //     catch (Exception ex) {
        //         return StatusCode(500, $"Internal server error: {ex.Message}");
        //     }
        // }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> Delete(int id) {
        //     var book = _context.Book.FirstOrDefault(x => x.Id == id); {
        //         if(book == null) {
        //             return NotFound();
        //         }

        //         _context.Book.Remove(book);
        //         await _context.SaveChangesAsync();
        //         return NoContent();
        //     }
        // }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks() {
            var books = await _bookRepo.GetAllAsync();
            var bookDto = books.Select(s => s.ToBookDto());
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdBook(int id) {
            var book = await _bookRepo.GetByIdAsync(id);
            if(book == null) {
                return NotFound();
            }
            return Ok(book.ToBookDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDto bookDto) {
            var bookModel = bookDto.CreateBookDto();
            await _bookRepo.CreateAsync(bookModel);
            return CreatedAtAction(nameof(GetByIdBook), new {id = bookModel.Id},bookModel.ToBookDto());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook(int id, UpdateBookDto bookDto) {
            var bookUpdate = await _bookRepo.UpdateAsync(id, bookDto);

            if(bookUpdate == null) {
                return NotFound();
            }
            return Ok(bookUpdate.ToBookDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id) {
            var book = await _bookRepo.DeleteAsync(id);

                if(book == null) {
                    return NotFound();
                }
                return NoContent();
        }
        

    }
}