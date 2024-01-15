using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentStudio.DataAccesLayer;
using RentStudio.Models;

namespace RentStudio.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly RentDbContext _context;

        public EmployeeController(RentDbContext context)
        {
            _context = context;
        }

        [HttpGet] //folosit pt requesturi de tip READ-citim/luam date din baza
        public IActionResult GetEmployees()
        {
            var employees = _context.Employees.Include(x=>x.Hotel).ToList();
            return Ok(employees);
        }

        [HttpPost] //folosit pt requesturi de tip WRITE-scriem date in baza(inserez o linie noua in tabela employees);HttpReq e format din Header si Body
        public IActionResult AddEmployee([FromBody] EmployeeDTO employee)
        {
            var entity=new Employee();
            entity.FirstName= employee.FirstName;
            entity.LastName= employee.LastName;
            entity.Age= employee.Age; //sau entity.Age= 18;
            entity.Gender= employee.Gender;
            entity.Salary= employee.Salary;
            entity.Position = employee.Position;
            entity.HotelId = employee.HotelId;
            _context.Employees.Add(entity);
            _context.SaveChanges(); //se salveaza datele in baza, orice modificare facuta de request nu se salveaza pana la SaveChanges
            return Ok(employee);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] EmployeeShortDTO updatedEmployee)
        {
            var existingEmployee = _context.Employees.Find(id);
            if (existingEmployee == null)
                return NotFound();

            existingEmployee.FirstName = updatedEmployee.FirstName;
            existingEmployee.Position = updatedEmployee.Position;
            // Update other properties as needed

            _context.SaveChanges();
            return Ok(existingEmployee);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
                return NotFound();

            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return Ok(employee);
        }
    }
}
