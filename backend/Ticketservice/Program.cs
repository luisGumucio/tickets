using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Ticketservice.Config;
using Ticketservice.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt =>
     {
         opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
     });
var dbHost = "localhost";
var dbName = "master";
var dbPassword = "Pepesan123";
var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};TrustServerCertificate=true;";
builder.Services.AddDbContext<ConnectionDatabase>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddScoped<ITicketService, TicketManagmentservice>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
