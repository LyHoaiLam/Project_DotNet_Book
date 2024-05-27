
using api.Dtos.Customer;
using api.models;

namespace api.Mappers {
    public static class CustomerMapper {
        public static CustomerDto ToCustomerDto(this Customer customerModel) {
                Console.WriteLine("---------------------------------------------------------------");
                // Console.WriteLine(bookModel.Id, bookModel.Title, bookModel.Description, bookModel.Amount);
                    Console.WriteLine("Id: {0}, Name: {1}, Description: {2}, YearOfBirth: {3}, National: {4}", 
                    customerModel.Id, customerModel.Name, customerModel.Description, customerModel.YearOfBirth, customerModel.National);
                Console.WriteLine("---------------------------------------------------------------");
        
                return new CustomerDto {
                    Id = customerModel.Id,
                    Name = customerModel.Name,
                    Description = customerModel.Description,
                    YearOfBirth = customerModel.YearOfBirth,
                    National = customerModel.National
                };
        }


        public static Customer CreateBookDto(this CreateCustomerDto customerDto) {
            return new Customer {
                Name = customerDto.Name,
                Description = customerDto.Description,
                YearOfBirth = customerDto.YearOfBirth,
                National = customerDto.National   
            };
        }

    }
}