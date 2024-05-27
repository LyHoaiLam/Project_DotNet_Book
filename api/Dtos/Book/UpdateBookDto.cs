namespace api.Dtos.Book {
    public class UpdateBookDto {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Amount { get; set; }
    }
}