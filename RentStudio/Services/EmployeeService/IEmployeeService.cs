using RentStudio.DataAccesLayer;
using RentStudio.Models.DTOs;
using RentStudio.Models.DTOs.Responses;
using RentStudio.Repositories.EmployeeRepository;

namespace RentStudio.Services.EmployeeService
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDTO> GetEmployees(FilterEmployeeDTO filterEmployeeDTO);
        public SalaryResponseDTO GetEmployeeSalary(int EmployeeId);
        void AddEmployee(EmployeeDTO employeeDto);
        void UpdateEmployee(int id, EmployeeShortDTO updatedEmployee);
        void DeleteEmployee(int id);
        bool Save();
        IEnumerable<GroupedEmployeesDTO> GetEmployeesGroupedByPosition();
        IEnumerable<EmployeeDTO> GetEmployeesAtHotel(int hotelId);
        IEnumerable<EmployeeWithHotelDTO> GetEmployeesWithHotels();
        string GetEmployeePositionByIdAsync(int employeeId);
        Task<List<string>> GetEmployeePositionsByIdsAsync(List<int> employeeIds);
        Employee GetEmployeeById(int employeeId);

    }
}
