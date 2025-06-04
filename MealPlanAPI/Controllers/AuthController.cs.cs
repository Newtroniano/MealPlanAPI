using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            Console.WriteLine("Login chamado");

            if (loginDto == null)
            {
                Console.WriteLine("loginDto é nulo");
                return BadRequest("Dados inválidos");
            }

            Console.WriteLine($"Tentativa de login para: {loginDto.Email}");

            var user = await _authService.ValidateUserAsync(loginDto.Email, loginDto.Password);

            if (user == null)
            {
                Console.WriteLine("Usuário não encontrado ou senha incorreta");
                return Unauthorized("Credenciais inválidas");
            }

            var role = user.Role.ToString();
            Console.WriteLine($"Usuário validado. Role: {role}");

            var token = _authService.GenerateToken(user.Email, role);
            Console.WriteLine($"Token gerado: {token}");

            return Ok(new { token });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro interno: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
            return StatusCode(500, new { message = "Erro interno", detail = ex.Message, stackTrace = ex.StackTrace });
        }
    }
}

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}