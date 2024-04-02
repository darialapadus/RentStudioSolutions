using RentStudio.DataAccesLayer;
using RentStudio.Models.DTOs;

namespace RentStudio.Repositories.EmployeeRepository
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees();
        IEnumerable<Employee> GetEmployees(FilterEmployeeDTO filters);
        void AddEmployee(EmployeeDTO employeeDto);
        void UpdateEmployee(int id, EmployeeShortDTO updatedEmployee);
        void DeleteEmployee(int id);
        bool Save();
        IEnumerable<GroupedEmployeesDTO> GetEmployeesGroupedByPosition();
        IEnumerable<EmployeeDTO> GetEmployeesAtHotel(int hotelId);
        IEnumerable<EmployeeWithHotelDTO> GetEmployeesWithHotels();
        string GetEmployeePositionByIdAsync(int employeeId);
        Task<List<string>> GetEmployeePositionsByIdsAsync(List<int> employeeIds);
        int GetHotelIdByEmployeeId(int employeeId);
        Employee GetEmployeeById(int employeeId);

    }
    public class EmployeeWithHotelDTO
    {
        public EmployeeDTO Employee { get; set; }
        public HotelDTO Hotel { get; set; }
    }
}
