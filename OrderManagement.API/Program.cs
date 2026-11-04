using Microsoft.EntityFrameworkCore;
using OrderManagement.Application;
using OrderManagement.Infrastructure;
using OrderManagement.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Register Application Layer
builder.Services.AddApplication();

// Register Infrastructure Layer
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply migrations and insert seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<OrderDbContext>();

    await SeedData.InitializeAsync(context);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();