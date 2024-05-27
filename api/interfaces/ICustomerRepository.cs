using api.Dtos.Customer;
using api.models;

namespace api.interfaces {
    public interface ICustomerRepository {
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(int id);
        Task<Customer> CreateAsync(Customer customer);
        Task<Customer> UpdateAsync(int id, UpdateCustomer customerDto);
        Task<Customer> DeteleAsync(int id);
    }
}