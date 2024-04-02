using Microsoft.AspNetCore.Mvc;
using RentStudio.Services.ReportService;

namespace RentStudio.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IReportService _reportService;

        public DashboardController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("total-salaries/{hotelId}")]
        public IActionResult GetTotalSalariesForHotelFromDate(int hotelId, [FromQuery] DateTime startDate)
        {
            if (startDate == DateTime.MinValue)
                return BadRequest("Please provide a valid start date.");

            decimal totalSalaries = _reportService.GetTotalSalariesForHotelFromDate(hotelId, startDate);
            return Ok(totalSalaries);
        }
    }

}
