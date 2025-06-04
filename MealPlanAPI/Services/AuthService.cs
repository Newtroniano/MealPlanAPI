using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        Console.WriteLine("AuthService criado com UserManager");
    }

    public async Task<ApplicationUser> ValidateUserAsync(string email, string password)
    {
        Console.WriteLine($"Iniciando validação do usuário: {email}");

        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            Console.WriteLine($"Usuário com email {email} não encontrado.");
            return null;
        }

        Console.WriteLine($"Usuário encontrado: {user.Email}. Verificando senha...");

        var passwordValid = await _userManager.CheckPasswordAsync(user, password);

        if (passwordValid)
        {
            Console.WriteLine("Senha correta.");
            return user;
        }
        else
        {
            Console.WriteLine("Senha incorreta.");
            return null;
        }
    }

    public string GenerateToken(string email, string role)
    {
        Console.WriteLine($"Gerando token para o usuário {email} com papel {role}");

        var secretKey = "sua-chave-secreta-muito-segura-e-complexa";
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, email),
        new Claim(ClaimTypes.Role, role),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

        // 4. Criar o token JWT
        var token = new JwtSecurityToken(
            issuer: "MealPlanAPI",          
            audience: "MealPlanAPIUsers",   
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1), 
            signingCredentials: creds
        );

        // 5. Gerar string do token JWT
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        Console.WriteLine($"Token gerado: {tokenString}");

        return tokenString;
    }
}
