using RentStudio.Models.DTOs.Responses;

namespace RentStudio.Services.SalaryService
{
    public interface ISalaryService
    {
        double CalculateSalary(DateTime startDate, string employeePosition);
       /* void AddSalary(SalaryDTO salaryDto);
        void UpdateSalary(int id, SalaryDTO updatedSalary);
        void DeleteSalary(int id);*/
    }
}
