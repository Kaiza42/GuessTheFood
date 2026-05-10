using GuessTheFood.api.Application.Interfaces.Services;
using GuessTheFood.api.Application.Services;
using GuessTheFood.Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IIngredientService, IngredientService>();

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

app.MapControllers();

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