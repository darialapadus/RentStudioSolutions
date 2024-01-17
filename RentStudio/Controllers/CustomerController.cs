using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // GROUPBY pentru a grupa clientii in functie de oras.
        [HttpGet("customers/grouped-by-city")]
        public IActionResult GetCustomersGroupedByCity()
        {
            var customersGroupedByCity = _context.Customers
                .GroupBy(c => c.City)
                .Select(group => new
                {
                    City = group.Key,
                    Customers = group.ToList()
                })
                .ToList();

            return Ok(customersGroupedByCity);
        }

        // WHERE pentru a obtine toti clientii cu un anumit nume.
        [HttpGet("customers-with-first-name/{firstName}")]
        public IActionResult GetCustomersWithFirstName(string firstName)
        {
            var customersWithFirstName = _context.Customers
                .Where(c => c.FirstName == firstName)
                .ToList();

            return Ok(customersWithFirstName);
        }

        // JOIN intre Customers si Reservations pentru a obtine informatiile despre clienti impreuna cu datele despre rezervari.
        [HttpGet("customers-with-reservations")]
        public IActionResult GetCustomersWithReservations()
        {
            var customersWithReservations = _context.Customers
                .Join(
                    _context.Reservations,
                    customer => customer.CustomerId,
                    reservation => reservation.CustomerId,
                    (customer, reservation) => new
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

            return Ok(customersWithReservations);
        }


        // INCLUDE pentru a incarca toate detaliile despre clienti impreuna cu informatiile despre rezervarile existente.
        [HttpGet("customers-with-details")]
        public IActionResult GetCustomersWithDetails()
        {
            var customersWithDetails = _context.Customers
                .Include(c => c.Reservations)
                .Select(customer => new CustomerDTO
                {
                    CustomerId = customer.CustomerId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    City = customer.City,
                    Reservations = customer.Reservations.Select(reservation => new ReservationDTO
                    {
                        ReservationId = reservation.ReservationId,
                        CheckInDate = reservation.CheckInDate,
                        CheckOutDate = reservation.CheckOutDate,
                        NumberOfRooms = reservation.NumberOfRooms,
                        NumberOfGuests = reservation.NumberOfGuests,
                        Status = reservation.Status,
                        PaymentMethod = reservation.PaymentMethod,
                        CustomerId = reservation.CustomerId
                    }).ToList()
                })
                .ToList();

            return Ok(customersWithDetails);
        }

    }
}
