using RentStudio.DataAccesLayer;
using RentStudio.Models.DTOs;
using RentStudio.Repositories.CustomerRepository;

namespace RentStudio.Services.CustomerService
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

        public IEnumerable<CustomerDTO> GetCustomers(FilterCustomerDTO filterCustomerDTO)
        {
            var anyCustomerFilter = AnyCustomerFilter(filterCustomerDTO);
            var customers = anyCustomerFilter ? _customerRepository.GetCustomers(filterCustomerDTO) : _customerRepository.GetCustomers();
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
            var entity = new Customer
            {
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                Email = customerDto.Email,
                Phone = customerDto.Phone,
                City = customerDto.City
            };
            _customerRepository.AddCustomer(entity);
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

        private bool AnyCustomerFilter(FilterCustomerDTO filterCustomerDTO)
        {
            if (filterCustomerDTO == null)
            {
                return false;
            }
            if (!string.IsNullOrEmpty(filterCustomerDTO.LastName) ||
                !string.IsNullOrEmpty(filterCustomerDTO.City) ||
                !string.IsNullOrEmpty(filterCustomerDTO.Email) ||
                !string.IsNullOrEmpty(filterCustomerDTO.Phone))

            {
                return true;
            }
            return false;
        }
    }
}