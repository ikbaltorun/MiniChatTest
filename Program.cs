using Microsoft.EntityFrameworkCore;
using MiniChatTest.Models;

var builder = WebApplication.CreateBuilder(args);//uygulama çalýþmasý için ön hazýrlýk yapmak
var env = builder.Environment; //iþletim sistemine ben þu an hangi ortamdayým diye sorduðu kýsýmdýr
//kýsaca development veye production olup olmadýðýna burda bakýlýr

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//ConnectionStrings bölümündeki DefaultConnection deðerini getir
//yani öcne hangþ ortamda çalýþtýðýný öðrenicek development ise appsettings.Development.json içine girip
//ConnectionStrings bölümündeki DefaultConnection deðerine bakýcak ve bize getiricek
Console.WriteLine($"{env.EnvironmentName} {connectionString}");//çalýþtýðý ortamý ekrana yazdýrýr


// Add services to the container.
builder.Services.AddControllers();//ben Controller kullanacaðým gerekli servisleri ekle
builder.Services.AddEndpointsApiExplorer();//endpointapiexplorer kullancaðým servislerini ekle
builder.Services.AddSwaggerGen();//swaggergen kullanýcam servisleri ekle

// SQLite Veritabaný Baðlantýsý
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=ecommerce.db"));
//ASP.NET Core, AppDbContext'i servis olarak kaydet. Ýleride biri isterse ona ver.


var app = builder.Build();//hazýrlýk bitti uygulama çalýþmaya hazýr

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