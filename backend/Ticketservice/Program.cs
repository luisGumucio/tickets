using System.Text.Json.Serialization;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Ticketservice.Config;
using Ticketservice.Services;
using Ticketservice.Consumer;
using Ticketservice.Producers;

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
builder.Services.AddScoped<ITicketProducer, TicketProducer>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        builder.WithOrigins("http://localhost:4200/")
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowedToAllowWildcardSubdomains());
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<TicketConsumer>();
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
    {
        config.Host(new Uri(RabbitMqConsts.RabbitMqRootUri), h =>
        {
            h.Username(RabbitMqConsts.UserName);
            h.Password(RabbitMqConsts.Password);
        });

        config.ReceiveEndpoint("ticketUpdateQueue", ep =>
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(r => r.Interval(2, 100));
            ep.ConfigureConsumer<TicketConsumer>(provider);
        });
    }));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseCors("AllowSpecificOrigin");
app.MapControllers();

app.Run();
