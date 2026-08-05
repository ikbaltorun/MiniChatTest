using Microsoft.EntityFrameworkCore;
using MiniChatTest.Models;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

Console.WriteLine($"{env.EnvironmentName} {connectionString}");
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SQLite Veritabaný Baðlantýsý
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=ecommerce.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();