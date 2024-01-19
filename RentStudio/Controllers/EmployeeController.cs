using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentStudio.DataAccesLayer;
using RentStudio.Models;
using RentStudio.Repositories;
using RentStudio.Services;

namespace RentStudio.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = _employeeService.GetEmployees();
            return Ok(employees);
        }

        [HttpPost]
        public IActionResult AddEmployee([FromBody] EmployeeDTO employeeDto)
        {
            _employeeService.AddEmployee(employeeDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] EmployeeShortDTO updatedEmployee)
        {
            _employeeService.UpdateEmployee(id, updatedEmployee);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            _employeeService.DeleteEmployee(id);
            return Ok();
        }

        [HttpGet("employees/grouped-by-position")]
        public IActionResult GetEmployeesGroupedByPosition()
        {
            var employeesGroupedByPosition = _employeeService.GetEmployeesGroupedByPosition();
            return Ok(employeesGroupedByPosition);
        }

        [HttpGet("employees-at-hotel/{hotelId}")]
        public IActionResult GetEmployeesAtHotel(int hotelId)
        {
            var employeesAtHotel = _employeeService.GetEmployeesAtHotel(hotelId);
            return Ok(employeesAtHotel);
        }

        [HttpGet("employees-with-hotels")]
        public IActionResult GetEmployeesWithHotels()
        {
            var employeesWithHotels = _employeeService.GetEmployeesWithHotels();
            return Ok(employeesWithHotels);
        }
    }

}
