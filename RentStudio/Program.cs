using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RentStudio.DataAccesLayer;
using RentStudio.Helpers;
using RentStudio.Helpers.JwtUtils;
using RentStudio.Repositories.CustomerRepository;
using RentStudio.Repositories.EmployeeRepository;
using RentStudio.Repositories.HotelRepository;
using RentStudio.Repositories.ReservationDetailRepository;
using RentStudio.Repositories.ReservationRepository;
using RentStudio.Repositories.RoomRepository;
using RentStudio.Repositories.RoomTypeRepository;
using RentStudio.Repositories.UserRepository;
using RentStudio.Services.CustomerService;
using RentStudio.Services.EmployeeService;
using RentStudio.Services.HotelService;
using RentStudio.Services.ReservationDetailService;
using RentStudio.Services.ReservationService;
using RentStudio.Services.RoomService;
using RentStudio.Services.RoomTypeService;
using RentStudio.Services.UserService;

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

builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomService, RoomService>();

builder.Services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
builder.Services.AddScoped<IRoomTypeService, RoomTypeService>();


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IJwtUtils, JwtUtils>();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<JwtMiddleware>();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.UseAuthorization();

app.MapControllers();

app.Run();
