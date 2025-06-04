using MealPlanAPI.Middleware;
using MealPlanAPI.Services.Patient;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DotNetEnv.Env.Load();
string dbConnection = Environment.GetEnvironmentVariable("DB_CONNECTION")
    ?? throw new Exception("DB_CONNECTION não configurada no .env");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(dbConnection));

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IFoodService, FoodService>();
builder.Services.AddScoped<IMealPlanService, MealPlanService>();


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();


builder.Services.AddScoped<AuthService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<JwtMiddleware>();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
