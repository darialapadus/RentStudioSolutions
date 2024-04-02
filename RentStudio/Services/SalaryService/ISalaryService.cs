using RentStudio.Models.DTOs.Responses;

namespace RentStudio.Services.SalaryService
{
    public interface ISalaryService
    {
        double CalculateSalary(DateTime startDate, string employeePosition);
  
    }
}
