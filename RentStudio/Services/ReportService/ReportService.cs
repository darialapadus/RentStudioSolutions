using RentStudio.DataAccesLayer;
using RentStudio.Services.SalaryService;

namespace RentStudio.Services.ReportService
{
    public class ReportService : IReportService
    {
        private readonly RentDbContext _context;
        private readonly ISalaryService _salaryService;

        public ReportService(RentDbContext context, ISalaryService salaryService)
        {
            _context = context;
            _salaryService = salaryService;

        }

        public decimal GetTotalSalariesForHotelFromDate(int hotelId, DateTime startDate)
        {
            var employees = _context.Employees
                .Where(e => e.HotelId == hotelId && e.StartDate >= startDate)
                .ToList(); 

            var totalSalaries = employees
                .Sum(e => _salaryService.CalculateSalary(e.StartDate, e.Position));

            return (decimal)totalSalaries;
        }


    }
}
