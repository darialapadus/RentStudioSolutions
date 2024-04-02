namespace RentStudio.Services.ReportService
{
    public interface IReportService
    {
        decimal GetTotalSalariesForHotelFromDate(int hotelId, DateTime startDate);
    }
}
