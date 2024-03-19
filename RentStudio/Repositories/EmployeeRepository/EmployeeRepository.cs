using Microsoft.EntityFrameworkCore;
using RentStudio.DataAccesLayer;
using RentStudio.Models.DTOs;

namespace RentStudio.Repositories.EmployeeRepository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly RentDbContext _context;

        public EmployeeRepository(RentDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees.Include(e => e.Hotel).ToList();
        }
        //de facut si cu stored procedures
        public IEnumerable<Employee> GetEmployees(FilterEmployeeDTO filters)
        {
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrEmpty(filters.FirstName))
            {
                query = query.Where(x => x.FirstName == filters.FirstName);
            }
            if (!string.IsNullOrEmpty(filters.Gender))
            {
                query = query.Where(x => x.Gender == filters.Gender);
            }
            if (!string.IsNullOrEmpty(filters.Position))
            {
                query = query.Where(x => x.Position == filters.Position);
            }
            if (filters.Salary > 0)
            {
                query = query.Where(x => x.Salary == filters.Salary);
            }
            var result = query.ToList();
            return result;

        }

        bool IEmployeeRepository.Save()
        {
            throw new NotImplementedException();
        }

        public void AddEmployee(EmployeeDTO employeeDto)
        {
            var entity = new Employee
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Age = employeeDto.Age,
                Gender = employeeDto.Gender,
                Salary = employeeDto.Salary,
                Position = employeeDto.Position,
                HotelId = employeeDto.HotelId
            };

            _context.Employees.Add(entity);
            _context.SaveChanges();
        }

        public void UpdateEmployee(int id, EmployeeShortDTO updatedEmployee)
        {
            var existingEmployee = _context.Employees.Find(id);
            if (existingEmployee == null)
            {
                throw new KeyNotFoundException("Employee not found");
            }

            existingEmployee.FirstName = updatedEmployee.FirstName;
            existingEmployee.Position = updatedEmployee.Position;

            _context.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }

        public IEnumerable<GroupedEmployeesDTO> GetEmployeesGroupedByPosition()
        {
            return _context.Employees
                .Include(e => e.Hotel)
                .ToList()
                .GroupBy(e => e.Position)
                .Select(group => new GroupedEmployeesDTO
                {
                    Position = group.Key,
                    Employees = group.Select(e => new EmployeeDTO
                    {
                        EmployeeId = e.EmployeeId,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        Gender = e.Gender,
                        Age = e.Age,
                        Position = e.Position,
                        Salary = e.Salary,
                        HotelId = e.HotelId,
                    }).ToList()
                })
                .ToList();
        }

        public IEnumerable<EmployeeDTO> GetEmployeesAtHotel(int hotelId)
        {
            return _context.Employees
                .Where(e => e.HotelId == hotelId)
                .Select(e => new EmployeeDTO
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Gender = e.Gender,
                    Age = e.Age,
                    Position = e.Position,
                    Salary = e.Salary,
                    HotelId = e.HotelId,
                })
                .ToList();
        }

        public IEnumerable<EmployeeWithHotelDTO> GetEmployeesWithHotels()
        {
            return _context.Employees
                .Join(
                    _context.Hotels,
                    employee => employee.HotelId,
                    hotel => hotel.HotelId,
                    (employee, hotel) => new EmployeeWithHotelDTO
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
        }

        public async Task<string> GetEmployeePositionByIdAsync(int employeeId)
        {
            var position = await _context.Employees
                .Where(e => e.EmployeeId == employeeId)
                .Select(e => e.Position)
                .FirstOrDefaultAsync();
            if (position == null)
            {
                throw new KeyNotFoundException($"Employee with ID {employeeId} not found");
            }
            return position;
        }

        public async Task<List<string>> GetEmployeePositionsByIdsAsync(List<int> employeeIds)
        {
            var positions = await _context.Employees
                .Where(e => employeeIds.Contains(e.EmployeeId))
                .Select(e => e.Position)
                .ToListAsync();
            if (positions == null || positions.Count == 0)
            {
                throw new KeyNotFoundException("No positions found for the provided employee IDs");
            }
            return positions;
        }

    }
}