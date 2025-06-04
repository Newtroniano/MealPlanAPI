using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
    public UserRole Role { get; set; }
}

public enum UserRole
{
    ADMIN,
    NUTRITIONIST
}