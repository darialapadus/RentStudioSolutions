using RentStudio.DataAccesLayer;
using RentStudio.Models.DTOs;
using RentStudio.Models.DTOs.Responses;
using RentStudio.Repositories.EmployeeRepository;
using RentStudio.Services.HotelService;
using RentStudio.Services.SalaryService;

namespace RentStudio.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ISalaryService _salaryService;
        private readonly IHotelService _hotelService;

        public EmployeeService(IEmployeeRepository employeeRepository, ISalaryService salaryService, IHotelService hotelService)
        {
            _employeeRepository = employeeRepository;
            _salaryService = salaryService;
            _hotelService = hotelService;
        }

        bool IEmployeeService.Save()
        {
            throw new NotImplementedException();
        }
        public SalaryResponseDTO GetEmployeeSalary(int employeeId)
        {
            var employeePosition = _employeeRepository.GetEmployeePositionByIdAsync(employeeId);
            /* var allEmployees = _employeeRepository.GetEmployees();          //de inlocuit cu noua metoda+modificarile de rigoare
             var startDate = allEmployees.FirstOrDefault(e => e.EmployeeId == employeeId).StartDate;
             var employeeName = allEmployees.FirstOrDefault(e => e.EmployeeId == employeeId).FirstName; //in serviciul de EmployeeRepository, EmployeeService trebuie sa adaugam GetEmployeeById */
            var employee = _employeeRepository.GetEmployeeById(employeeId);
            var startDate = employee.StartDate;
            var employeeName = employee.FirstName;
            var salary = _salaryService.CalculateSalary(startDate, employeePosition);
            var hotelId = _employeeRepository.GetHotelIdByEmployeeId(employeeId);
            var hotelName = _hotelService.GetHotelNameById(hotelId);
            var numberOfRooms = _hotelService.GetNumberOfRooms(hotelId);
            var responseDto = new SalaryResponseDTO
            {
                Salary = salary,
                HotelName = hotelName,
                Position = employeePosition,
                NumberOfRooms = numberOfRooms,
                EmployeeName = employeeName
            };

            return responseDto;
        }
        public Employee GetEmployeeById(int employeeId)
        {
            return _employeeRepository.GetEmployeeById(employeeId);
        }

        public IEnumerable<EmployeeDTO> GetEmployees(FilterEmployeeDTO filterEmployeeDTO)
        {
            var anyFilters = AnyFilter(filterEmployeeDTO);
            var employees = anyFilters ? _employeeRepository.GetEmployees(filterEmployeeDTO) : _employeeRepository.GetEmployees();
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

        public string GetEmployeePositionByIdAsync(int employeeId)
        {
            return _employeeRepository.GetEmployeePositionByIdAsync(employeeId);
        }

        public async Task<List<string>> GetEmployeePositionsByIdsAsync(List<int> employeeIds)
        {
            return await _employeeRepository.GetEmployeePositionsByIdsAsync(employeeIds);
        }

        private bool AnyFilter(FilterEmployeeDTO filterEmployeeDTO)
        {
            if (filterEmployeeDTO == null)
                return false;

            if (filterEmployeeDTO.Salary != 0 ||
                !string.IsNullOrEmpty(filterEmployeeDTO.FirstName) ||
                !string.IsNullOrEmpty(filterEmployeeDTO.Gender) ||
                !string.IsNullOrEmpty(filterEmployeeDTO.Position))
            {
                return true;
            }
            return false;
        }
    }
}
