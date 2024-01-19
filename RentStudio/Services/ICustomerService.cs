﻿using RentStudio.Models;

namespace RentStudio.Services
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDTO> GetCustomers();
        void AddCustomer(CustomerDTO customerDto);
        void UpdateCustomer(int id, CustomerShortDTO updatedCustomer);
        void DeleteCustomer(int id);
        bool Save();
        IEnumerable<GroupedCustomersDTO> GetCustomersGroupedByCity();
        IEnumerable<CustomerDTO> GetCustomersWithFirstName(string firstName);
        IEnumerable<CustomerWithReservationsDTO> GetCustomersWithReservations();
    }
}
