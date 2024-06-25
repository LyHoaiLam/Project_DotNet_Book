using api.Data;
using api.Dtos.Customer;
using api.Helpers;
using api.interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.controllers {
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase {
        private readonly ApplacationDBContext _context;
        private readonly ICustomerRepository _customerRepo;

        public CustomerController(ApplacationDBContext context, ICustomerRepository customerRepo) {
            _context = context;
            _customerRepo = customerRepo;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query) {

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var customers = await _customerRepo.GetAllAsync(query);
            var customerDto = customers.Select(c => c.ToCustomerDto());
            return Ok(customers);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdCustomer(int id) {
            
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = await _customerRepo.GetByIdAsync(id);
            if(customer == null) {
                return NotFound();
            }
            return Ok(customer.ToCustomerDto());
        }


        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto customerDto) {
            
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var customer = customerDto.CreateBookDto();
            await _customerRepo.CreateAsync(customer);
            return CreatedAtAction(nameof(GetByIdCustomer), new {id = customer.Id}, customer.ToCustomerDto());
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute]int id, UpdateCustomer updateCustomer) {
            var customerUpdate = await _customerRepo.UpdateAsync(id, updateCustomer);
            
            if(customerUpdate == null) {
                return NotFound();
            }
            return NoContent();
        }


        [HttpDelete("{id:int}")]
            public async Task<IActionResult> DeleteCustomer([FromRoute] int id) {
                
                if(!ModelState.IsValid)
                return BadRequest(ModelState);

                var customerDelete = await _customerRepo.DeteleAsync(id);
                if (customerDelete == null) {
                    return NotFound();
                }
                return NoContent();
        }


 
    }
}