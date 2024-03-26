using Microsoft.AspNetCore.Mvc;
using RentStudio.Models.DTOs;
using RentStudio.Models.DTOs.Responses;
using RentStudio.Services.EmployeeService;

namespace RentStudio.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("employees/salary")]
        public IActionResult GetEmployeeSalary(int EmployeeId)
        {
            if (EmployeeId <= 0)
                return BadRequest("EmployeeId must be greater than 0");

            SalaryResponseDTO salary = _employeeService.GetEmployeeSalary(EmployeeId);

            return Ok(salary);
        }

        /*[HttpGet] 
        public IActionResult GetEmployees()
        {
            var employees = _employeeService.GetEmployees(new FilterEmployeeDTO());
            return Ok(employees);
        }*/

        [HttpGet]
        public IActionResult GetEmployees([FromQuery] FilterEmployeeDTO filterEmployeeDTO)
        {
            var employees = _employeeService.GetEmployees(filterEmployeeDTO);
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
        public ActionResult<string> GetEmployeePosition(int employeeId)
        {
            var position = _employeeService.GetEmployeePositionByIdAsync(employeeId);
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
