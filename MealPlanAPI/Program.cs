using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DotNetEnv.Env.Load(); 
string dbConnection = Environment.GetEnvironmentVariable("DB_CONNECTION")
    ?? throw new Exception("DB_CONNECTION não configurada no .env");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(dbConnection));

var app = builder.Build(); 

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();