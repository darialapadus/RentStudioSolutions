using Microsoft.AspNetCore.Mvc;
using RentStudio.DataAccesLayer;

namespace RentStudio.Controllers
{
    public class ReservationController : BaseController
    {
        private readonly RentDbContext _context;
        public ReservationController(RentDbContext context)
        {
            _context = context;
        }

        // GET: api/Student
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            return _context.Students.ToList();
        }
    }
}