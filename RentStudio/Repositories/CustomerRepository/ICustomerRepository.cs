﻿using RentStudio.DataAccesLayer;
using RentStudio.Models.DTOs;

namespace RentStudio.Repositories.CustomerRepository
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
        IEnumerable<Customer> GetCustomers(FilterCustomerDTO filterCustomerDTO);

        void AddCustomer(Customer customerDto);
        void UpdateCustomer(int id, CustomerShortDTO updatedCustomer);
        void DeleteCustomer(int id);
        bool Save();
        IEnumerable<GroupedCustomersDTO> GetCustomersGroupedByCity();
        IEnumerable<CustomerDTO> GetCustomersWithFirstName(string firstName);
        IEnumerable<CustomerWithReservationsDTO> GetCustomersWithReservations();
    }
}
