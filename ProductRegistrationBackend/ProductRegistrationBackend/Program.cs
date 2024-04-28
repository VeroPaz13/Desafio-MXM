using Microsoft.EntityFrameworkCore;
using ProductRegistrationBackend.Data;
using Serilog;

var log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("serilog/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
log.Information("Start aplication");
Log.Logger = log;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog();
builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Lista de Produtos", Version = "v1" });
});

builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
    .EnableSensitiveDataLogging()
    .LogTo(Console.WriteLine, LogLevel.Debug)
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lista de Produtos"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.Run();