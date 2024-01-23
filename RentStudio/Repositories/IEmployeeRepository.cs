using RentStudio.DataAccesLayer;
using RentStudio.Models;

namespace RentStudio.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees();
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
    public class EmployeeWithHotelDTO
    {
        public EmployeeDTO Employee { get; set; }
        public HotelDTO Hotel { get; set; }
    }
}

