using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSE.Identidade.API.Extensions;
using NSE.Identidade.API.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace NSE.Identidade.API.Controllers;

/// <summary>
/// Controlador responsável pela autenticação de usuários.
/// </summary>
[ApiController]
[Route("api/identidade")]

public class AuthController : Controller  // Later we can fix this providing a base class with common behavior
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AppSettings _appSettings;
    
    /// <summary>
    /// Inicializa uma nova instância do controlador AuthController.
    /// </summary>
    /// <param name="userManager">Gerenciador de usuários.</param>
    /// <param name="signInManager">Gerenciador de login.</param>
    /// <param name="appSettings">Configurações da aplicação.</param>
    public AuthController(UserManager<IdentityUser> userManager, 
                        SignInManager<IdentityUser> signInManager, 
                        IOptions<AppSettings> appSettings)
    {   
        _userManager = userManager;
        _signInManager = signInManager;
        _appSettings = appSettings.Value;
    }
    
    
    /// <summary>
    /// Cria uma nova conta de usuário.
    /// </summary>
    /// <param name="userRegister">Informações de registro do usuário.</param>
    /// <returns>Resultado do processo de criação de conta.</returns>
    [HttpPost("Criar-conta")]
    
    public async Task<IActionResult> Register(UserRegister userRegister)
    {
        System.Diagnostics.Debugger.Break();
        
        if (!ModelState.IsValid) return BadRequest();   
        
        var user = new IdentityUser
        {
            UserName = userRegister.Email,
            Email = userRegister.Email,
            EmailConfirmed = true
        };
        
        var result = await _userManager.CreateAsync(user, userRegister.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return Ok(GenerateJwt(userRegister.Email));
        }
        
        return BadRequest();
    }
    
    /// <summary>
    /// Autentica um usuário.
    /// </summary>
    /// <param name="userLogin">Informações de login do usuário.</param>
    /// <returns>Resultado do processo de autenticação.</returns>
    [HttpPost("Autenticar")]
    public async Task<IActionResult> Login(UserLogin userLogin)
    {
        if (!ModelState.IsValid) return BadRequest();

        var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);

        if (result.Succeeded)
        {
            return Ok(await GenerateJwt(userLogin.Email));
        }
        
        return BadRequest();
    }
    
    /// <summary>
    /// Gera um token de autenticação JWT.
    /// </summary>
    /// <param name="email">E-mail do usuário.</param>
    /// <returns>Token de autenticação JWT.</returns>
    private async Task<UserAnswerLogin> GenerateJwt(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var claims = await _userManager.GetClaimsAsync(user);
        var userRoles = await _userManager.GetRolesAsync(user);
        
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
        
        foreach (var userRole in userRoles)
        {
            claims.Add(new Claim("role", userRole));
        }

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _appSettings.Issuer,
            Audience = _appSettings.ValidIn,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });
        
        var encodedToken = tokenHandler.WriteToken(token);
        
        var response = new UserAnswerLogin
        {
            AccessToken = encodedToken,
            ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds,
            UserToken = new UserToken
            {
                Id = user.Id,
                Email = user.Email,
                Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
            }
        };
        
        return response;
    }
    
    private static long ToUnixEpochDate(DateTime date)
    => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
}