using RentStudio.Models.DTOs;

namespace RentStudio.Services.CustomerService
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDTO> GetCustomers(FilterCustomerDTO filterCustomerDTO);
        void AddCustomer(CustomerDTO customerDto);
        void UpdateCustomer(int id, CustomerShortDTO updatedCustomer);
        void DeleteCustomer(int id);
        bool Save();
        IEnumerable<GroupedCustomersDTO> GetCustomersGroupedByCity();
        IEnumerable<CustomerDTO> GetCustomersWithFirstName(string firstName);
        IEnumerable<CustomerWithReservationsDTO> GetCustomersWithReservations();
    }
}
