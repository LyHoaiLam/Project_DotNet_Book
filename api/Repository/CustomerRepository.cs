using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Book;
using api.Dtos.Customer;
using api.interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository {
    public class CustomerRepository : ICustomerRepository {
        private readonly ApplacationDBContext _context;

        public CustomerRepository(ApplacationDBContext context) {
            _context = context;
        }


        public async Task<List<Customer>> GetAllAsync() {

            // return await _context.Customer.ToListAsync();
            return await _context.Customer.Include(b => b.Books).ToListAsync();
        }


        public async Task<Customer> GetByIdAsync(int id) {

            // return await _context.Customer.FindAsync(id);
            return await _context.Customer.Include(b => b.Books).FirstOrDefaultAsync(i => i.Id == id);
        }


        public async Task<Customer> CreateAsync(Customer customer) {
            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();

            return customer;
        }


        public async Task<Customer> UpdateAsync(int id, UpdateCustomer customerDto)
        {
            var customerUpdate = await _context.Customer.FirstOrDefaultAsync(c => c.Id == id);
            if(customerUpdate == null) {
                return null;
            }
            customerUpdate.Name = customerDto.Name;
            customerUpdate.Description = customerDto.Description;
            customerUpdate.YearOfBirth = customerDto.YearOfBirth;
            customerUpdate.National = customerDto.National;
            await _context.SaveChangesAsync();

            return customerUpdate;
        }


        // public async Task<Customer> DeteleAsync(int id)
        // {
        //     var customerDetele = await _context.Customer.FirstOrDefaultAsync(c => c.Id == id);
        //     if(customerDetele == null) {
        //         return null;
        //     }

        //     _context.Customer.Remove(customerDetele);
        //     await _context.SaveChangesAsync();

        //     return customerDetele;
        // }


 
        public async Task<Customer> DeteleAsync(int id) {
            using var transaction = await _context.Database.BeginTransactionAsync();
            
            try {
                var customerToDelete = await _context.Customer.Include(c => c.Books)
                                                            .FirstOrDefaultAsync(c => c.Id == id);
                if (customerToDelete == null) {
                    return null;
                }
                // Remove related books
                _context.Book.RemoveRange(customerToDelete.Books);
                // Remove customer
                _context.Customer.Remove(customerToDelete);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return customerToDelete;
            }
            catch (Exception) {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public Task<bool> CustomerExists(int id) {
            // throw new NotImplementedException();
            return _context.Customer.AnyAsync(s => s.Id == id);
        }

     
    }
}