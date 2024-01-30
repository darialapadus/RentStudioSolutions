using RentStudio.Models.DTOs;
using RentStudio.Repositories.EmployeeRepository;

namespace RentStudio.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        bool IEmployeeService.Save()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmployeeDTO> GetEmployees()
        {
            var employees = _employeeRepository.GetEmployees();
            return employees.Select(e => new EmployeeDTO
            {
                EmployeeId = e.EmployeeId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Gender = e.Gender,
                Age = e.Age,
                Position = e.Position,
                Salary = e.Salary,
                HotelId = e.HotelId
            }).ToList();
        }

        public void AddEmployee(EmployeeDTO employeeDto)
        {
            _employeeRepository.AddEmployee(employeeDto);
        }

        public void UpdateEmployee(int id, EmployeeShortDTO updatedEmployee)
        {
            _employeeRepository.UpdateEmployee(id, updatedEmployee);
        }

        public void DeleteEmployee(int id)
        {
            _employeeRepository.DeleteEmployee(id);
        }

        public IEnumerable<GroupedEmployeesDTO> GetEmployeesGroupedByPosition()
        {
            return _employeeRepository.GetEmployeesGroupedByPosition();
        }

        public IEnumerable<EmployeeDTO> GetEmployeesAtHotel(int hotelId)
        {
            return _employeeRepository.GetEmployeesAtHotel(hotelId);
        }

        public IEnumerable<EmployeeWithHotelDTO> GetEmployeesWithHotels()
        {
            return _employeeRepository.GetEmployeesWithHotels();
        }

        public async Task<string> GetEmployeePositionByIdAsync(int employeeId)
        {
            return await _employeeRepository.GetEmployeePositionByIdAsync(employeeId);
        }

        public async Task<List<string>> GetEmployeePositionsByIdsAsync(List<int> employeeIds)
        {
            return await _employeeRepository.GetEmployeePositionsByIdsAsync(employeeIds);
        }

    }
}
