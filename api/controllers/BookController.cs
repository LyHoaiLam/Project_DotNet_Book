using api.Data;
using api.Dtos.Book;
using api.interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.controllers {
    [Route("api/Book")]
    [ApiController]
    public class BookController : ControllerBase {

        private readonly ApplacationDBContext _context;
        private readonly IBookRepository _bookRepo;
        private readonly ICustomerRepository _customerRepo;

        public BookController(IBookRepository bookRepo, ICustomerRepository customerRepo) {
;
            _bookRepo = bookRepo;
            _customerRepo = customerRepo;
        }
        


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllBooks() {

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var books = await _bookRepo.GetAllAsync();
            var bookDto = books.Select(s => s.ToBookDto());
            return Ok(bookDto);
        }



        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetByIdBook([FromRoute] int id) {

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = await _bookRepo.GetByIdAsync(id);
            if(book == null) {
                return NotFound();
            }
            return Ok(book.ToBookDto());
        }



        // [HttpPost("{customerId:int}")]
        [HttpPost]
        [Route("{customerId:int}")]

        public async Task<IActionResult> CreateBook([FromRoute]int customerId, CreateBookDto bookDto) {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!await _customerRepo.CustomerExists(customerId)) {
                return BadRequest("Customer Does Not Exist 3999");
            }

            var bookModel = bookDto.CreateBookDto(customerId);
            await _bookRepo.CreateAsync(bookModel);
            return CreatedAtAction(nameof(GetByIdBook), new {id = bookModel.Id}, bookModel.ToBookDto());
        }



        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, UpdateBookDto bookDto) {
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var bookUpdate = await _bookRepo.UpdateAsync(id, bookDto);

            if(bookUpdate == null) {
                return NotFound();
            }
            return Ok(bookUpdate.ToBookDto());
        }



        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id) {
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = await _bookRepo.DeleteAsync(id);
            if(book == null) {
                return NotFound();
            }
            return NoContent();
        }
        

    }
}