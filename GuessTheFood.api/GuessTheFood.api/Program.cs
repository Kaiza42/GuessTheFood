using GuessTheFood.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () =>
    {
        return Results.Ok(new
        {
            name = "GuessTheFood API",
            status = "running"
        });
    })
    .WithName("Root");

app.MapGet("/health", () =>
    {
        return Results.Ok(new
        {
            status = "ok"
        });
    })
    .WithName("HealthCheck");

app.Run();