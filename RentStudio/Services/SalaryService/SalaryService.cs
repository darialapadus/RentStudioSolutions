using RentStudio.DataAccesLayer;
using RentStudio.Models.DTOs.Responses;
using RentStudio.Models.Enums;

namespace RentStudio.Services.SalaryService
{
    public class SalaryService : ISalaryService
    {
        private readonly RentDbContext _context;

        public SalaryService(RentDbContext context)
        {
            _context = context;
        }

        public double CalculateSalary(DateTime startDate, string employeePosition)
        {
            var roleId = 0;
            Models.Enums.EmployeeRole role;
            if (Enum.TryParse(employeePosition, out role))
            {
                roleId = (int)role;
            }

            //var employeeRole = _context.EmployeeRoles.FirstOrDefault(e => e.Name == employeePosition);
            var employeeRole = _context.EmployeeRoles.FirstOrDefault(e => e.Id == roleId);

            if (employeeRole != null)
            {
                var baseSalary = employeeRole.BaseSalary;
                var netSalary = baseSalary - (baseSalary * (int)Taxes.SalaryTax / 100);
                var daysWorked = (DateTime.Now - startDate).Days;
                var salary = netSalary;
                decimal bonus = 0.1m;
                while(daysWorked > 365*2)
                {
                    daysWorked -= 365;
                    salary = salary + (salary * bonus);
                }
                return (double)salary;
            }   
            return 0;
        }
    }
}
