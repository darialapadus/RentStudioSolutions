﻿using Microsoft.EntityFrameworkCore;
using RentStudio.DataAccesLayer;
using RentStudio.Models.DTOs;

namespace RentStudio.Repositories.CustomerRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly RentDbContext _context;

        public CustomerRepository(RentDbContext context)
        {
            _context = context;
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.Include(c => c.Reservations).ToList();
        }
        public IEnumerable<Customer> GetCustomers(FilterCustomerDTO filterCustomerDTO)
        {
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(filterCustomerDTO.LastName))
            {
                query = query.Where(x => x.LastName == filterCustomerDTO.LastName);
            }
            if (!string.IsNullOrEmpty(filterCustomerDTO.Email))
            {
                query = query.Where(x => x.Email == filterCustomerDTO.Email);
            }
            if (!string.IsNullOrEmpty(filterCustomerDTO.Phone))
            {
                query = query.Where(x => x.Phone == filterCustomerDTO.Phone);
            }
            if (!string.IsNullOrEmpty(filterCustomerDTO.City))
            {
                query = query.Where(x => x.City == filterCustomerDTO.City);
            }

            return query.ToList(); 
        }
        public void AddCustomer(Customer entity)
        {
            _context.Customers.Add(entity);
            _context.SaveChanges();
        }

        public void UpdateCustomer(int id, CustomerShortDTO updatedCustomer)
        {
            var existingCustomer = _context.Customers.Find(id);
            if (existingCustomer == null)
            {
                throw new KeyNotFoundException("Customer not found");
            }

            existingCustomer.Email = updatedCustomer.Email;
            existingCustomer.Phone = updatedCustomer.Phone;

            _context.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }

        public IEnumerable<GroupedCustomersDTO> GetCustomersGroupedByCity()
        {
            return _context.Customers
                .GroupBy(c => c.City)
                .Select(group => new GroupedCustomersDTO
                {
                    City = group.Key,
                    Customers = group.ToList()
                })
                .ToList();
        }

        public IEnumerable<CustomerDTO> GetCustomersWithFirstName(string firstName)
        {
            return _context.Customers
                .Where(c => c.FirstName == firstName)
                .Select(c => new CustomerDTO
                {
                    CustomerId = c.CustomerId,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    Phone = c.Phone,
                    City = c.City
                })
                .ToList();
        }

        public IEnumerable<CustomerWithReservationsDTO> GetCustomersWithReservations()
        {
            return _context.Customers
                .Join(
                    _context.Reservations,
                    customer => customer.CustomerId,
                    reservation => reservation.CustomerId,
                    (customer, reservation) => new CustomerWithReservationsDTO
                    {
                        Customer = new CustomerDTO
                        {
                            CustomerId = customer.CustomerId,
                            FirstName = customer.FirstName,
                            LastName = customer.LastName,
                            Email = customer.Email,
                            Phone = customer.Phone,
                            City = customer.City
                        },
                        Reservation = new ReservationDTO
                        {
                            ReservationId = reservation.ReservationId,
                            CheckInDate = reservation.CheckInDate,
                            CheckOutDate = reservation.CheckOutDate,
                            NumberOfRooms = reservation.NumberOfRooms,
                            NumberOfGuests = reservation.NumberOfGuests,
                            Status = reservation.Status,
                            PaymentMethod = reservation.PaymentMethod,
                            CustomerId = reservation.CustomerId
                        }
                    })
                .ToList();
        }

    }

}
