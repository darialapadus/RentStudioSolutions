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
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;
using RentStudio.Services.SalaryService;

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

builder.Services.AddScoped<ISalaryService, SalaryService>();

builder.Services.AddScoped<IJwtUtils, JwtUtils>();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {

        };
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            RoleClaimType = ClaimTypes.Role,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Laboratorul9_2023_DAW_Softbinator")),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));

    // Add more policies as needed
});


builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiPlayground", Version = "v1" });
        c.AddSecurityDefinition(
            "token",
            new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer",
                In = ParameterLocation.Header,
                Name = HeaderNames.Authorization
            }
        );
        c.AddSecurityRequirement(
            new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "token"
                        },
                    },
                    Array.Empty<string>()
                }
            }
        );
    }
);

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
    app.UseSwaggerUI();
}
app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");


app.MapControllers();

app.Run();
