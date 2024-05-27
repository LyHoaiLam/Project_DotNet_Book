using api.Dtos.Book;
using api.models;

namespace api.interfaces {
    public interface IBookRepository {
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<Book?> CreateAsync(Book book);
        Task<Book?> UpdateAsync(int id, UpdateBookDto bookDto);
        Task<Book?> DeleteAsync(int id);
    }
}