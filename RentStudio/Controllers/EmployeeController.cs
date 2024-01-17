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
            var employees = _context.Employees.ToList(); //var employees = _context.Employees.Include(x=>x.Hotel).ToList(); inseamna ca aduce si datele despre hoteluri intr-o singura cerere
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

        //GROUPBY pentru a grupa angajatii in functie de pozitia pe care o ocupa.
        [HttpGet("employees/grouped-by-position")]
        public IActionResult GetEmployeesGroupedByPosition()
        {
            var employeesGroupedByPosition = _context.Employees
                .GroupBy(e => e.Position)
                .Select(group => new
                {
                    Position = group.Key,
                    Employees = group.ToList()
                })
                .ToList();

            return Ok(employeesGroupedByPosition);
        }

        //WHERE pentru a obtine toti angajatii care lucreaza la un anumit hotel.
        [HttpGet("employees-at-hotel/{hotelId}")]
        public IActionResult GetEmployeesAtHotel(int hotelId)
        {
            var employeesAtHotel = _context.Employees
                .Where(e => e.HotelId == hotelId)
                .ToList();

            return Ok(employeesAtHotel);
        }

        //JOIN intre Employees si Hotels pentru a obtine informatiile despre angajati impreuna cu datele despre hoteluri.
        [HttpGet("employees-with-hotels")]
        public IActionResult GetEmployeesWithHotels()
        {
            var employeesWithHotels = _context.Employees
                .Join(
                    _context.Hotels,
                    employee => employee.HotelId,
                    hotel => hotel.HotelId,
                    (employee, hotel) => new
                    {
                        Employee = new EmployeeDTO
                        {
                            EmployeeId = employee.EmployeeId,
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            Gender = employee.Gender,
                            Age = employee.Age,
                            Position = employee.Position,
                            Salary = employee.Salary,
                            HotelId = employee.HotelId
                        },
                        Hotel = new HotelDTO
                        {
                            HotelId = hotel.HotelId,
                            Name = hotel.Name,
                            Rating = hotel.Rating,
                            Address = hotel.Address
                        }
                    })
                .ToList();

            return Ok(employeesWithHotels);
        }


        //INCLUDE pentru a incarca toate detaliile despre angajati impreuna cu informatiile despre hoteluri.
        [HttpGet("employees-with-details")]
        public IActionResult GetEmployeesWithDetails()
        {
            var employeesWithDetails = _context.Employees
                .Include(e => e.Hotel)
                .Select(employee => new
                {
                    Employee = new EmployeeDTO
                    {
                        EmployeeId = employee.EmployeeId,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Gender = employee.Gender,
                        Age = employee.Age,
                        Position = employee.Position,
                        Salary = employee.Salary,
                        HotelId = employee.HotelId
                    },
                    Hotel = new HotelDTO
                    {
                        HotelId = employee.Hotel.HotelId,
                        Name = employee.Hotel.Name,
                        Rating = employee.Hotel.Rating,
                        Address = employee.Hotel.Address
                    }
                })
                .ToList();

            return Ok(employeesWithDetails);
        }


    }

}
