using api.Dtos.Book;
using api.models;

namespace api.Mappers {
    public static class BookMappers {
         public static BookDto ToBookDto(this Book bookModel) {
                Console.WriteLine("---------------------------------------------------------------");
                // Console.WriteLine(bookModel.Id, bookModel.Title, bookModel.Description, bookModel.Amount);
                    Console.WriteLine("Id: {0}, Title: {1}, Description: {2}, Amount: {3}", bookModel.Id, bookModel.Title, bookModel.Description, bookModel.Amount);
                Console.WriteLine("---------------------------------------------------------------");

            return new BookDto {
                Id = bookModel.Id,
                Title = bookModel.Title,
                Description = bookModel.Description,
                Price = bookModel.Price,
                Amount = bookModel.Amount

            };
        }

        public static Book CreateBookDto(this CreateBookDto bookDto, int customerId) {
            return new Book {
                Title = bookDto.Title,
                Description = bookDto.Description,
                Price = bookDto.Price,
                Amount = bookDto.Amount,
                CustomerId = customerId  
            };
        }


        public static Book UpdateBookDto(this UpdateBookDto bookDto) {
            return new Book {
                Title = bookDto.Title,
                Description = bookDto.Description,
                Price = bookDto.Price,
                Amount = bookDto.Amount,
            };
        }
    }
}