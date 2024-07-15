using api.Data;
using api.Dtos.Book;
using api.interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository {
    public class BookRepository : IBookRepository {
        private readonly ApplacationDBContext _context;

        public BookRepository(ApplacationDBContext context) {
            _context = context;
        }

        public async Task<List<Book>> GetAllAsync() {
            return await _context.Book.ToListAsync();
        }


        public async Task<Book?> GetByIdAsync(int id) {
            return await _context.Book.FindAsync(id);
        }


        public async Task<Book?> CreateAsync(Book book) {
            await _context.Book.AddAsync(book);
            await _context.SaveChangesAsync();
            
            return book;
        }


        public async Task<Book?> UpdateAsync(int id, UpdateBookDto bookDto) {
            var bookUpdate = await _context.Book.FirstOrDefaultAsync(b => b.Id == id);
            if(bookUpdate == null) {
                return null;
            }

            bookUpdate.Title = bookDto.Title;
            bookUpdate.Description = bookDto.Description;
            bookUpdate.Price = bookDto.Price;
            bookUpdate.Amount = bookDto.Amount;
            await _context.SaveChangesAsync();

            return bookUpdate;
        }


        public async Task<Book?> DeleteAsync(int id) {
            var bookModel = await _context.Book.FirstOrDefaultAsync(b => b.Id == id);
            if(bookModel == null) {
                return null;
            }
            // _context.Remove(book);
            _context.Book.Remove(bookModel);
            await _context.SaveChangesAsync();

            return bookModel;
        }
      
       
    }
}