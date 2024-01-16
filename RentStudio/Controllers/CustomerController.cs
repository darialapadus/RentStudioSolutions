using Microsoft.AspNetCore.Mvc;
using RentStudio.DataAccesLayer;
using RentStudio.Models;

namespace RentStudio.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly RentDbContext _context;

        public CustomerController(RentDbContext context)
        {
            _context = context;
        }

        [HttpGet] //folosit pt requesturi de tip READ-citim/luam date din baza
        public IActionResult GetCustomers()
        {
            var customers = _context.Customers.ToList();
            return Ok(customers);
        }

        [HttpPost] //folosit pt requesturi de tip WRITE-scriem date in baza(inserez o linie noua in tabela customers);HttpReq e format din Header si Body
        public IActionResult AddCustomers([FromBody] CustomerDTO customer)
        {
            var entity = new Customer();
            entity.FirstName = customer.FirstName;
            entity.LastName = customer.LastName;
            entity.Email = customer.Email;
            entity.Phone = customer.Phone;  
            entity.City = customer.City;
            _context.Customers.Add(entity);
            _context.SaveChanges();
            return Ok(customer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] CustomerShortDTO updatedCustomer)
        {
            var existingCustomer = _context.Customers.Find(id);
            if (existingCustomer == null)
                return NotFound();

            existingCustomer.Email = updatedCustomer.Email;
            existingCustomer.Phone = updatedCustomer.Phone;

            _context.SaveChanges();
            return Ok(existingCustomer);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
                return NotFound();

            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return Ok(customer);
        }
    }
}
