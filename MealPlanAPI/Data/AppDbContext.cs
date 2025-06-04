using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


    private string HasherPassword(string password)
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        return hasher.HashPassword(null, password);
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Food> Foods { get; set; }
    public DbSet<MealPlan> MealPlans { get; set; }
    public DbSet<MealPlanFood> MealPlanFoods { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Patient>().HasQueryFilter(p => !p.IsDeleted);

        modelBuilder.Entity<Food>().HasQueryFilter(p => !p.IsDeleted);

        modelBuilder.Entity<MealPlan>().HasQueryFilter(p => !p.IsDeleted);

        modelBuilder.Entity<MealPlanFood>().HasQueryFilter(p => !p.IsDeleted);

        // Configuração do usuário admin inicial (exemplo)
        modelBuilder.Entity<ApplicationUser>().HasData(
        new ApplicationUser
        {
            Id = "1",
            UserName = "admin@mealplan.com",
            NormalizedUserName = "ADMIN@MEALPLAN.COM",
            Email = "admin@mealplan.com",
            NormalizedEmail = "ADMIN@MEALPLAN.COM",
            EmailConfirmed = true,
            PasswordHash = HasherPassword("Admin@123"), 
            SecurityStamp = Guid.NewGuid().ToString(),
            FullName = "Administrador do Sistema",
            Role = UserRole.ADMIN
        }
        );


    }
}