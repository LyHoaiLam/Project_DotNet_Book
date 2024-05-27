namespace api.Dtos.Book {
    public class CreateBookDto {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Amount { get; set; }
    }
}