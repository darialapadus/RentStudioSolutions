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

        //GROUPBY pentru a grupa angajatii in functie de pozitia pe care o ocupa.
        [HttpGet("employees/grouped-by-position")]
        public IActionResult GetEmployeesGroupedByPosition()
        {
            var employeesGroupedByPosition = _employeeService.GetEmployeesGroupedByPosition();
            return Ok(employeesGroupedByPosition);
        }

        //WHERE pentru a obtine toti angajatii care lucreaza la un anumit hotel.
        [HttpGet("employees-at-hotel/{hotelId}")]
        public IActionResult GetEmployeesAtHotel(int hotelId)
        {
            var employeesAtHotel = _employeeService.GetEmployeesAtHotel(hotelId);
            return Ok(employeesAtHotel);
        }

        //JOIN intre Employees si Hotels pentru a obtine informatiile despre angajati impreuna cu datele despre hoteluri.
        [HttpGet("employees-with-hotels")]
        public IActionResult GetEmployeesWithHotels()
        {
            var employeesWithHotels = _employeeService.GetEmployeesWithHotels();
            return Ok(employeesWithHotels);
        }

        //GET, dar iau decat o valoare dupa un id
        [HttpGet("{employeeId}/position")]
        public async Task<ActionResult<string>> GetEmployeePosition(int employeeId)
        {
            var position = await _employeeService.GetEmployeePositionByIdAsync(employeeId);
            return Ok(position);
        }

        //GET, dar iau val dupa o lista de id
        [HttpGet("positions")]
        public async Task<ActionResult<List<string>>> GetEmployeePositions([FromQuery] List<int> employeeIds)
        {
            var positions = await _employeeService.GetEmployeePositionsByIdsAsync(employeeIds);
            return Ok(positions);
        }

    }
}
