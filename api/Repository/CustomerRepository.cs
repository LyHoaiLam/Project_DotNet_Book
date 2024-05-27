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

            return await _context.Customer.ToListAsync();
        }


        public async Task<Customer> GetByIdAsync(int id) {

            return await _context.Customer.FindAsync(id);
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


        public async Task<Customer> DeteleAsync(int id)
        {
            var customerDetele = await _context.Customer.FirstOrDefaultAsync(c => c.Id == id);
            if(customerDetele == null) {
                return null;
            }

            _context.Remove(customerDetele);
            await _context.SaveChangesAsync();

            return customerDetele;
        }   

      
    }
}