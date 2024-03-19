using RentStudio.Models.DTOs;
using RentStudio.Repositories.EmployeeRepository;

namespace RentStudio.Services.EmployeeService
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDTO> GetEmployees(FilterEmployeeDTO filterEmployeeDTO);
        void AddEmployee(EmployeeDTO employeeDto);
        void UpdateEmployee(int id, EmployeeShortDTO updatedEmployee);
        void DeleteEmployee(int id);
        bool Save();
        IEnumerable<GroupedEmployeesDTO> GetEmployeesGroupedByPosition();
        IEnumerable<EmployeeDTO> GetEmployeesAtHotel(int hotelId);
        IEnumerable<EmployeeWithHotelDTO> GetEmployeesWithHotels();
        Task<string> GetEmployeePositionByIdAsync(int employeeId);
        Task<List<string>> GetEmployeePositionsByIdsAsync(List<int> employeeIds);
    }
}
