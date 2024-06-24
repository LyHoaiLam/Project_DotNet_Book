namespace api.Dtos.Book {
    public class BookDto {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Amount { get; set; }

        public int? CustomerId { get; set; }

    }
}