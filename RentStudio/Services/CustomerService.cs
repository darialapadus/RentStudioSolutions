using RentStudio.Models;
using RentStudio.Repositories;

namespace RentStudio.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public bool Save()
        {
            return _customerRepository.Save();
        }

        public IEnumerable<CustomerDTO> GetCustomers()
        {
            var customers = _customerRepository.GetCustomers();
            return customers.Select(c => new CustomerDTO
            {
                CustomerId = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone,
                City = c.City
            }).ToList();
        }

        public void AddCustomer(CustomerDTO customerDto)
        {
            _customerRepository.AddCustomer(customerDto);
        }

        public void UpdateCustomer(int id, CustomerShortDTO updatedCustomer)
        {
            _customerRepository.UpdateCustomer(id, updatedCustomer);
        }

        public void DeleteCustomer(int id)
        {
            _customerRepository.DeleteCustomer(id);
        }

        public IEnumerable<GroupedCustomersDTO> GetCustomersGroupedByCity()
        {
            return _customerRepository.GetCustomersGroupedByCity();
        }

        public IEnumerable<CustomerDTO> GetCustomersWithFirstName(string firstName)
        {
            var customers = _customerRepository.GetCustomersWithFirstName(firstName);
            return customers.Select(c => new CustomerDTO
            {
                CustomerId = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone,
                City = c.City
            }).ToList();
        }

        public IEnumerable<CustomerWithReservationsDTO> GetCustomersWithReservations()
        {
            return _customerRepository.GetCustomersWithReservations();
        }

    }
}