namespace api.Dtos.Customer {
    public class UpdateCustomer {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime YearOfBirth { get; set; }
        public string? National { get; set; }
    }
}