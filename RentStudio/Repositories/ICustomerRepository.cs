using RentStudio.DataAccesLayer;
using RentStudio.Models;

namespace RentStudio.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
        void AddCustomer(CustomerDTO customerDto);
        void UpdateCustomer(int id, CustomerShortDTO updatedCustomer);
        void DeleteCustomer(int id);
        bool Save();
        IEnumerable<GroupedCustomersDTO> GetCustomersGroupedByCity();
        IEnumerable<CustomerDTO> GetCustomersWithFirstName(string firstName);
        IEnumerable<CustomerWithReservationsDTO> GetCustomersWithReservations();
    }
}
