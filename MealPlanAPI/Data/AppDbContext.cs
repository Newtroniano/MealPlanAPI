using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Food> Foods { get; set; }
    public DbSet<MealPlan> MealPlans { get; set; }
    public DbSet<MealPlanFood> MealPlanFoods { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Patient>().HasQueryFilter(p => !p.IsDeleted);

        
    }
}