using api.Dtos.Book;

namespace api.Dtos.Customer {
    public class CustomerDto {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime YearOfBirth { get; set; }
        public string? National { get; set; }

        public List<BookDto>? Books { get; set; }


    }
}