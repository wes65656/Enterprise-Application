using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSE.Identidade.API.Extensions;
using NSE.Identidade.API.Models;

namespace NSE.Identidade.API.Controllers;

[Route("api/identidade")]
public class AuthController : MainController
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AppSettings _appSettings;

    public AuthController(UserManager<IdentityUser> userManager,
                          SignInManager<IdentityUser> signInManager,
                          IOptions<AppSettings> appSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _appSettings = appSettings.Value;
    }

    [HttpPost("Criar-conta")]
    public async Task<IActionResult> Register(UserRegister userRegister)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState); // Return detailed errors

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
            return CustomResponse(await GenerateJwt(user)); // Pass the user object for efficiency
        }

        foreach (var error in result.Errors)
        {
            AddError(error.Description);
        }

        return CustomResponse(result.Errors); // Return detailed errors
    }

    [HttpPost("Autenticar")]
    public async Task<IActionResult> Login(UserLogin userLogin)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState); // Return detailed errors

        var result =
            await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false,
                lockoutOnFailure: false);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(userLogin.Email);
            return CustomResponse(await GenerateJwt(user));
        }

        if (result.IsNotAllowed)
        {
            AddError("User is temporarily blocked for security reasons.");
            return CustomResponse();
        }
        
        AddError("User or password is invalid.");
        return CustomResponse(); // More descriptive feedback
    }
    
    private async Task<UserAnswerLogin> GenerateJwt(IdentityUser user)
    {
        var claims = await _userManager.GetClaimsAsync(user);

        // Using a list for efficiency, and correct claim handling.
        var userClaims = new List<Claim>();
        userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        userClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        userClaims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(),
            ClaimValueTypes.Integer64));
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            userClaims.Add(new Claim(ClaimTypes.Role, role)); // Correct Role claim type
        }

        claims.ToList().ForEach(c => userClaims.Add(c)); // Add existing claims

        var identityClaims = new ClaimsIdentity(userClaims);
        var token = CreateJwtToken(identityClaims);

        return new UserAnswerLogin
        {
            AccessToken = token,
            ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds,
            UserToken = new UserToken
            {
                Id = user.Id,
                Email = user.Email,
                Claims = userClaims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
            }
        };
    }


    private string CreateJwtToken(ClaimsIdentity identityClaims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _appSettings.Issuer,
            Audience = _appSettings.ValidIn,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });
        return tokenHandler.WriteToken(token);
    }

    private static long ToUnixEpochDate(DateTime date) =>
        (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
            .TotalSeconds);
}