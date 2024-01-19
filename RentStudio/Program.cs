using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RentStudio.DataAccesLayer;
using RentStudio.Repositories;
using RentStudio.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RentDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>(); 
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IHotelService, HotelService>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();

builder.Services.AddScoped<IReservationDetailRepository, ReservationDetailRepository>();
builder.Services.AddScoped<IReservationDetailService, ReservationDetailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
