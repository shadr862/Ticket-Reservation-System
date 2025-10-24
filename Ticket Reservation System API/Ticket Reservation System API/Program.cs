using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Ticket_Reservation_System_API.Data;
using Ticket_Reservation_System_API.Interfaces;
using Ticket_Reservation_System_API.Repositories;
using Ticket_Reservation_System_API.Services;

var builder = WebApplication.CreateBuilder(args);

// ========================
// Add Services
// ========================

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IBusRepository, BusRepository>();
builder.Services.AddScoped<IRouteRepository, RouteRepository>();
builder.Services.AddScoped<IBusScheduleRepository, BusScheduleRepository>();
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Application Services
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IBusService, BusService>();

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Ticket Reservation API",
        Version = "v1",
        Description = "API for Bus Ticket Reservation System"
    });
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy", policy =>
    {
        policy.WithOrigins("*")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// ========================
// Middleware
// ========================

app.UseHttpsRedirection();
app.UseCors("Policy");
app.UseAuthorization();

// Swagger Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Generate Swagger JSON
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ticket Reservation API V1");
        c.RoutePrefix = string.Empty; // Swagger available at root "/"
    });
}

// Map Controllers
app.MapControllers();

app.Run();
