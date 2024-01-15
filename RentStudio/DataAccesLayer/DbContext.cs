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
        //Aici vom specifica set-urile de date, corespondente cu tabele din baza de date, sub forma unor liste DbSet.
        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<RoomType> RoomTypes { get; set; }

        public DbSet<BookedRoom> BookedRooms { get; set; }

        public DbSet<ReservationDetail> ReservationDetails {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<RoomType>()
                .Property(rt => rt.Price)
                .HasColumnType("decimal(18, 2)");
        }


        //In DbContext vom avea si metoda ce configureaza Contextul, OnConfiguring, in care vom specifica ConnectionString-ul spre baza de date pe care o vom folosi
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(); //ConnectionString
        }*/

    }
}
