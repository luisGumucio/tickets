using System.Text.Json.Serialization;
using ManagmentService.Config;
using ManagmentService.Consumer;
using ManagmentService.Hubs;
using ManagmentService.Producer;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ITicketProducer, TicketProducer>();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<TicketConsumer>();
    x.UsingRabbitMq((context, config) =>
    {
        config.Host(new Uri(RabbitMqConsts.RabbitMqRootUri), h =>
        {
            h.Username(RabbitMqConsts.UserName);
            h.Password(RabbitMqConsts.Password);
        });
        config.ReceiveEndpoint("ticketQueue", ep =>
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(r => r.Interval(2, 100));
            ep.ConfigureConsumer<TicketConsumer>(context);
        });
    });
});

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

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
}).AddJsonProtocol(options =>
{
    options.PayloadSerializerOptions.Converters
       .Add(new JsonStringEnumConverter());
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowSpecificOrigin");
app.MapHub<ChatHub>("/chatHub");
app.MapHub<TicketHub>("/ticketHub");
app.MapHub<TicketChatHub>("/ticketChatHub");

app.Run();

