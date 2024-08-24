using Microsoft.EntityFrameworkCore;
using Serilog;
using SnakeNet_API.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
	.ReadFrom.Configuration(hostingContext.Configuration)
	.Enrich.FromLogContext()
	.WriteTo.Console());

// Add services to the container.
builder.Services.AddDbContext<DbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDummySql"));
});

builder.Services.AddHealthChecks();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHealthChecks("/api/health");

app.MapControllers();

app.Run();
