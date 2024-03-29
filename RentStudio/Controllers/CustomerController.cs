﻿using Microsoft.AspNetCore.Mvc;
using RentStudio.Models.DTOs;
using RentStudio.Services.CustomerService;

namespace RentStudio.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /*[HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _customerService.GetCustomers();
            return Ok(customers);
        }*/

        [HttpGet]
        public IActionResult GetCustomers([FromQuery] FilterCustomerDTO filterCustomerDTO)
        {
            var customers = _customerService.GetCustomers(filterCustomerDTO);
            return Ok(customers);
        }


        [HttpPost]  
        public IActionResult AddCustomer([FromBody] CustomerDTO customerDto)
        {
            _customerService.AddCustomer(customerDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] CustomerShortDTO updatedCustomer)
        {
            _customerService.UpdateCustomer(id, updatedCustomer);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            _customerService.DeleteCustomer(id);
            return Ok();
        }

        // GROUPBY pentru a grupa clientii in functie de oras.
        [HttpGet("grouped-by-city")]
        public IActionResult GetCustomersGroupedByCity()
        {
            var customersGroupedByCity = _customerService.GetCustomersGroupedByCity();
            return Ok(customersGroupedByCity);
        }

        // WHERE pentru a obtine toti clientii cu un anumit nume.
        [HttpGet("with-first-name/{firstName}")]
        public IActionResult GetCustomersWithFirstName(string firstName)
        {
            var customersWithFirstName = _customerService.GetCustomersWithFirstName(firstName);
            return Ok(customersWithFirstName);
        }

        // JOIN intre Customers si Reservations pentru a obtine informatiile despre clienti impreuna cu datele despre rezervari.
        [HttpGet("with-reservations")]
        public IActionResult GetCustomersWithReservations()
        {
            var customersWithReservations = _customerService.GetCustomersWithReservations();
            return Ok(customersWithReservations);
        }

    }
}


