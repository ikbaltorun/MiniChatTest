using Microsoft.SemanticKernel;
using MiniChatTest.Plugins;
using Microsoft.EntityFrameworkCore;
using MiniChatTest.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Semantic Kernel builder'ý oluþturuyoruz (Sadece bir kez!)
var kernelBuilder = Kernel.CreateBuilder();

// Ollama sunucusunu ve modelini ekliyoruz
kernelBuilder.AddOllamaChatCompletion(
    modelId: "llama3.1:8b",
    endpoint: new Uri("http://192.168.5.200:11434")
);

// 2. Yazdýðýmýz eklentiyi (Plugin) Kernel çantasýna tanýtýyoruz
kernelBuilder.Plugins.AddFromType<UserRegistrationPlugin>();

// 3. Kernel'ý inþa edip sisteme (Dependency Injection) kaydediyoruz
var kernel = kernelBuilder.Build();
builder.Services.AddSingleton(kernel);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=ecommerce.db"));
var app = builder.Build();

// Statik ve varsayýlan dosyalar (index.html için) API route'larýndan ÖNCE gelmelidir!
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();