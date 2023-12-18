using Microsoft.EntityFrameworkCore;

namespace RentStudio.DataAccesLayer
{
    public class RentDbContext : DbContext
    {
        public RentDbContext(DbContextOptions<RentDbContext> options) : base(options) 
        {
        }
        public DbSet<Student> Students { get; set; }

    }
}
