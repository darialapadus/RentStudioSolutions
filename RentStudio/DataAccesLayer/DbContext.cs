using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RentStudio.DataAccesLayer
{
    public class RentDbContext : DbContext
    {
        public RentDbContext(DbContextOptions<RentDbContext> options) : base(options) 
        {
        }
        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<RoomType> RoomTypes { get; set; }

        public DbSet<BookedRoom> BookedRooms { get; set; }

        public DbSet<ReservationDetail> ReservationDetails {  get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<EmployeeRole> EmployeeRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<RoomType>()
                .Property(rt => rt.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Reservations)
                .WithOne(r => r.Customer)
                .HasForeignKey(r => r.CustomerId);

            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18, 2)");
        }
        public DbSet<Payment> Payments { get; set; }

    }
}
