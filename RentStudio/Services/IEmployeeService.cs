using RentStudio.DataAccesLayer;
using RentStudio.Models;
using RentStudio.Repositories;

namespace RentStudio.Services
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDTO> GetEmployees();
        void AddEmployee(EmployeeDTO employeeDto);
        void UpdateEmployee(int id, EmployeeShortDTO updatedEmployee);
        void DeleteEmployee(int id);
        bool Save();
        IEnumerable<GroupedEmployeesDTO> GetEmployeesGroupedByPosition();
        IEnumerable<EmployeeDTO> GetEmployeesAtHotel(int hotelId);
        IEnumerable<EmployeeWithHotelDTO> GetEmployeesWithHotels();


    }
}
